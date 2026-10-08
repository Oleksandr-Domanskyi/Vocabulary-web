import { createRoot } from "react-dom/client";
import { useMemo, useState } from "react";
import type { Dispatch, SetStateAction } from "react";

import Header from "./layout/Header";
import Toolbar from "./layout/Toolbar";
import CardCollection from "./layout/CardCollection";
import type { ViewMode } from "./layout/CardCollection";
import WordModal from "./components/WordModal/WordModal";
import Hero from "./layout/Hero";
import SideBar from "./layout/SideBar";

import type { Word, WordStatus } from "./components/WordCard/WordCard";

import type { LevelFilter, StatusFilter } from "./layout/Toolbar";

import type { SidebarPage } from "./layout/SideBar";

import { WORDS } from "./data/words";

import styles from "../styles/Main.module.css";

type SlideDirection = "left" | "right";

const root = document.getElementById("root");

const Main = () => {
	const [known, setKnown] = useState<Set<number>>(new Set());
	const [unknown, setUnknown] = useState<Set<number>>(new Set());
	const [favorites, setFavorites] = useState<Set<number>>(new Set());
	const [archived, setArchived] = useState<Set<number>>(new Set());
	const [dismissed, setDismissed] = useState<Set<number>>(new Set());

	const [search, setSearch] = useState("");
	const [levelFilter, setLevelFilter] = useState<LevelFilter>("all");
	const [statusFilter, setStatusFilter] = useState<StatusFilter>("all");
	const [activePage, setActivePage] = useState<SidebarPage>("all");

	const [selectedWordId, setSelectedWordId] = useState<number | null>(null);

	const [slideDirection, setSlideDirection] = useState<SlideDirection>("left");

	const [viewMode, setViewMode] = useState<ViewMode>("grid");

	const getWordStatus = (id: number): WordStatus => {
		if (known.has(id)) return "known";
		if (unknown.has(id)) return "unknown";
		return "unseen";
	};

	const availableWords = useMemo(
		() => WORDS.filter((word) => !dismissed.has(word.id)),
		[dismissed],
	);

	const filteredWords = useMemo(() => {
		const query = search.trim().toLocaleLowerCase();

		return availableWords.filter((word) => {
			const id = word.id;
			const isArchived = archived.has(id);
			const status = getWordStatus(id);

			// Sidebar category
			let matchesPage = false;

			switch (activePage) {
				case "all":
					matchesPage = !isArchived;
					break;
				case "learning":
					matchesPage = !isArchived && status === "unknown";
					break;
				case "known":
					matchesPage = !isArchived && status === "known";
					break;
				case "favorites":
					matchesPage = !isArchived && favorites.has(id);
					break;
				case "archive":
					matchesPage = isArchived;
					break;
			}

			if (!matchesPage) return false;

			// Level
			const matchesLevel = levelFilter === "all" || word.level === levelFilter;

			if (!matchesLevel) return false;

			// Toolbar status
			let matchesStatus = false;

			switch (statusFilter) {
				case "all":
					matchesStatus = true;
					break;
				case "unseen":
					matchesStatus = status === "unseen";
					break;
				case "known":
					matchesStatus = status === "known";
					break;
				case "unknown":
					matchesStatus = status === "unknown";
					break;
				case "favorites":
					matchesStatus = favorites.has(id);
					break;
				case "archived":
					matchesStatus = isArchived;
					break;
			}

			if (!matchesStatus) return false;

			// Search through string fields of the word.
			const searchableText = Object.values(word)
				.filter((value): value is string => typeof value === "string")
				.join(" ")
				.toLocaleLowerCase();

			return searchableText.includes(query);
		});
	}, [
		availableWords,
		search,
		levelFilter,
		statusFilter,
		activePage,
		known,
		unknown,
		favorites,
		archived,
	]);

	const counts = useMemo(() => {
		const activeWords = availableWords.filter((word) => !archived.has(word.id));

		return {
			total: activeWords.length,
			learning: activeWords.filter((word) => unknown.has(word.id)).length,
			known: activeWords.filter((word) => known.has(word.id)).length,
			favorites: activeWords.filter((word) => favorites.has(word.id)).length,
			archived: availableWords.filter((word) => archived.has(word.id)).length,
		};
	}, [availableWords, known, unknown, favorites, archived]);

	const selectedWord =
		filteredWords.find((word) => word.id === selectedWordId) ?? null;

	const selectedWordIndex =
		selectedWord ?
			filteredWords.findIndex((word) => word.id === selectedWord.id)
		:	-1;

	const hasPrevious = selectedWordIndex > 0;

	const hasNext =
		selectedWordIndex >= 0 && selectedWordIndex < filteredWords.length - 1;

	const openCard = (id: number) => {
		setSlideDirection("left");
		setSelectedWordId(id);
	};

	const closeCard = () => {
		setSelectedWordId(null);
	};

	const toggleSetValue = (
		setter: Dispatch<SetStateAction<Set<number>>>,
		id: number,
	) => {
		setter((current) => {
			const updated = new Set(current);

			if (updated.has(id)) {
				updated.delete(id);
			} else {
				updated.add(id);
			}

			return updated;
		});
	};

	const toggleFavorite = (id: number) => {
		toggleSetValue(setFavorites, id);
	};

	const toggleArchive = (id: number) => {
		toggleSetValue(setArchived, id);
	};

	const changeStatus = (id: number, status: WordStatus) => {
		setKnown((current) => {
			const updated = new Set(current);

			if (status === "known") {
				updated.add(id);
			} else {
				updated.delete(id);
			}

			return updated;
		});

		setUnknown((current) => {
			const updated = new Set(current);

			if (status === "unknown") {
				updated.add(id);
			} else {
				updated.delete(id);
			}

			return updated;
		});
	};

	const previousWord = () => {
		if (!hasPrevious) return;

		setSlideDirection("right");
		setSelectedWordId(filteredWords[selectedWordIndex - 1].id);
	};

	const nextWord = () => {
		if (!hasNext) return;

		setSlideDirection("left");
		setSelectedWordId(filteredWords[selectedWordIndex + 1].id);
	};

	const dismissWord = (id: number) => {
		const currentIndex = filteredWords.findIndex((word) => word.id === id);

		if (currentIndex === -1) return;

		const next = filteredWords[currentIndex + 1];
		const previous = filteredWords[currentIndex - 1];

		setDismissed((current) => {
			const updated = new Set(current);
			updated.add(id);
			return updated;
		});

		if (next) {
			setSlideDirection("left");
			setSelectedWordId(next.id);
		} else if (previous) {
			setSlideDirection("right");
			setSelectedWordId(previous.id);
		} else {
			setSelectedWordId(null);
		}
	};

	const resetFilters = () => {
		setSearch("");
		setLevelFilter("all");
		setStatusFilter("all");
	};

	const navigate = (page: SidebarPage) => {
		setActivePage(page);
		setStatusFilter("all");
		setSelectedWordId(null);
	};

	const selectedWordStatus: WordStatus =
		selectedWord ? getWordStatus(selectedWord.id) : "unseen";

	return (
		<>
			<Header />

			<div className={styles.global}>
				<SideBar
					activePage={activePage}
					onNavigate={navigate}
					totalWords={counts.total}
					learningWords={counts.learning}
					knownWords={counts.known}
					favoriteWords={counts.favorites}
					archivedWords={counts.archived}
					dailyProgress={counts.known}
					dailyGoal={10}
				/>

				<main className={styles.content}>
					<Hero name="Sasha" />

					<Toolbar
						search={search}
						onSearchChange={setSearch}
						levelFilter={levelFilter}
						onLevelChange={setLevelFilter}
						statusFilter={statusFilter}
						onStatusChange={setStatusFilter}
						onReset={resetFilters}
						filteredWordsCount={filteredWords.length}
						totalWordsCount={counts.total}
						totalStudied={counts.known}
						viewMode={viewMode}
						onViewModeChange={setViewMode}
					/>

					<div key={viewMode} className={styles.viewTransition}>
						<CardCollection
							words={filteredWords}
							known={known}
							unknown={unknown}
							favorites={favorites}
							viewMode={viewMode}
							onOpenCard={openCard}
							onToggleFavorite={toggleFavorite}
						/>
					</div>
				</main>
			</div>

			{selectedWord && (
				<WordModal
					word={selectedWord}
					status={selectedWordStatus}
					isFavorite={favorites.has(selectedWord.id)}
					isArchived={archived.has(selectedWord.id)}
					slideDirection={slideDirection}
					onClose={closeCard}
					onToggleFavorite={toggleFavorite}
					onStatusChange={changeStatus}
					onPrevious={previousWord}
					onNext={nextWord}
					hasPrevious={hasPrevious}
					hasNext={hasNext}
					onArchive={toggleArchive}
					onDismiss={dismissWord}
				/>
			)}
		</>
	);
};

createRoot(root!).render(<Main />);
