import { createRoot } from "react-dom/client";
import { useState } from "react";

import Header from "./layout/Header";
import Toolbar from "./layout/Toolbar";
import CardCollection from "./layout/CardCollection";
import WordModal from "./components/WordModal/WordModal";

import type { WordStatus } from "./components/WordCard/WordCard";
import { WORDS } from "./data/words";

type SlideDirection = "left" | "right";

const root = document.getElementById("root");

const Main = () => {
	const [known, setKnown] = useState<Set<number>>(new Set());
	const [unknown, setUnknown] = useState<Set<number>>(new Set());
	const [favorites, setFavorites] = useState<Set<number>>(new Set());
	const [archived, setArchived] = useState<Set<number>>(new Set());
	const [dismissed, setDismissed] = useState<Set<number>>(new Set());
	const [selectedWordId, setSelectedWordId] = useState<number | null>(null);
	const [slideDirection, setSlideDirection] = useState<SlideDirection>("left");

	const visibleWords = WORDS.filter((word) => !dismissed.has(word.id));

	const selectedWord =
		visibleWords.find((word) => word.id === selectedWordId) ?? null;

	const selectedWordIndex =
		selectedWord ?
			visibleWords.findIndex((word) => word.id === selectedWord.id)
		:	-1;

	const hasPrevious = selectedWordIndex > 0;
	const hasNext =
		selectedWordIndex >= 0 && selectedWordIndex < visibleWords.length - 1;

	const openCard = (id: number) => {
		setSlideDirection("left");
		setSelectedWordId(id);
	};

	const closeCard = () => {
		setSelectedWordId(null);
	};

	const toggleSetValue = (
		setter: React.Dispatch<React.SetStateAction<Set<number>>>,
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

	const getWordStatus = (id: number): WordStatus => {
		if (known.has(id)) {
			return "known";
		}

		if (unknown.has(id)) {
			return "unknown";
		}

		return "unseen";
	};

	const previousWord = () => {
		if (!hasPrevious) {
			return;
		}

		setSlideDirection("right");
		setSelectedWordId(visibleWords[selectedWordIndex - 1].id);
	};

	const nextWord = () => {
		if (!hasNext) {
			return;
		}

		setSlideDirection("left");
		setSelectedWordId(visibleWords[selectedWordIndex + 1].id);
	};

	const dismissWord = (id: number) => {
		const currentIndex = visibleWords.findIndex((word) => word.id === id);

		if (currentIndex === -1) {
			return;
		}

		const nextWord = visibleWords[currentIndex + 1];
		const previousWord = visibleWords[currentIndex - 1];

		setDismissed((current) => {
			const updated = new Set(current);
			updated.add(id);
			return updated;
		});

		if (nextWord) {
			setSlideDirection("left");
			setSelectedWordId(nextWord.id);
			return;
		}

		if (previousWord) {
			setSlideDirection("right");
			setSelectedWordId(previousWord.id);
			return;
		}

		setSelectedWordId(null);
	};

	const selectedWordStatus: WordStatus =
		selectedWord ? getWordStatus(selectedWord.id) : "unseen";

	return (
		<>
			<Header />
			<Toolbar />
			<CardCollection
				words={visibleWords}
				known={known}
				unknown={unknown}
				favorites={favorites}
				onOpenCard={openCard}
				onToggleFavorite={toggleFavorite}
			/>
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
