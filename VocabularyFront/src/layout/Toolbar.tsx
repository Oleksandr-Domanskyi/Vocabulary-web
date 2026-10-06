import { useEffect, useRef, useState } from "react";

import styles from "./Toolbar.module.css";

type LevelFilter = "all" | "A2" | "B1" | "B2" | "C1";

type StatusFilter =
	| "all"
	| "unseen"
	| "known"
	| "unknown"
	| "favorites"
	| "archived";

interface StatusOption {
	value: StatusFilter;
	label: string;
	color: string;
}

const LEVELS: LevelFilter[] = ["all", "A2", "B1", "B2", "C1"];

const STATUS_OPTIONS: StatusOption[] = [
	{
		value: "all",
		label: "Все слова",
		color: "#a78bfa",
	},
	{
		value: "unseen",
		label: "Новые",
		color: "#94a3b8",
	},
	{
		value: "known",
		label: "Знаю",
		color: "#34d399",
	},
	{
		value: "unknown",
		label: "Учу",
		color: "#f87171",
	},
	{
		value: "favorites",
		label: "Избранное",
		color: "#fb7185",
	},
	{
		value: "archived",
		label: "Архив",
		color: "#60a5fa",
	},
];

const Toolbar = () => {
	const [search, setSearch] = useState("");

	const [levelFilter, setLevelFilter] = useState<LevelFilter>("all");

	const [statusFilter, setStatusFilter] = useState<StatusFilter>("all");

	const [levelMenuOpen, setLevelMenuOpen] = useState(false);

	const [statusMenuOpen, setStatusMenuOpen] = useState(false);

	const levelRef = useRef<HTMLDivElement>(null);
	const statusRef = useRef<HTMLDivElement>(null);

	/*
	 * Temporary data.
	 * Потом сюда можно передать реальные значения через props.
	 */
	const filteredWordsCount = 4;
	const totalWordsCount = 8;
	const totalStudied = 4;

	const progress = totalWordsCount > 0 ? totalStudied / totalWordsCount : 0;

	const circleLength = 72.26;

	const circleOffset = circleLength * (1 - progress);

	const activeStatus =
		STATUS_OPTIONS.find((status) => status.value === statusFilter) ??
		STATUS_OPTIONS[0];

	const filtersActive =
		search.length > 0 || levelFilter !== "all" || statusFilter !== "all";

	useEffect(() => {
		const handleOutsideClick = (event: MouseEvent) => {
			const target = event.target as Node;

			if (levelRef.current && !levelRef.current.contains(target)) {
				setLevelMenuOpen(false);
			}

			if (statusRef.current && !statusRef.current.contains(target)) {
				setStatusMenuOpen(false);
			}
		};

		document.addEventListener("mousedown", handleOutsideClick);

		return () => {
			document.removeEventListener("mousedown", handleOutsideClick);
		};
	}, []);

	const handleLevelMenu = () => {
		setLevelMenuOpen((current) => !current);
		setStatusMenuOpen(false);
	};

	const handleStatusMenu = () => {
		setStatusMenuOpen((current) => !current);
		setLevelMenuOpen(false);
	};

	const handleLevelSelect = (level: LevelFilter) => {
		setLevelFilter(level);
		setLevelMenuOpen(false);
	};

	const handleStatusSelect = (status: StatusFilter) => {
		setStatusFilter(status);
		setStatusMenuOpen(false);
	};

	const resetFilters = () => {
		setSearch("");
		setLevelFilter("all");
		setStatusFilter("all");

		setLevelMenuOpen(false);
		setStatusMenuOpen(false);
	};

	return (
		<div className={styles.toolbar}>
			{/* SEARCH */}

			<div className={`${styles.search} ${search ? styles.searchActive : ""}`}>
				<svg
					className={styles.searchIcon}
					width="16"
					height="16"
					viewBox="0 0 16 16"
					fill="none"
					aria-hidden="true">
					<circle
						cx="7"
						cy="7"
						r="4.75"
						stroke="currentColor"
						strokeWidth="1.4"
					/>

					<path
						d="M10.5 10.5l3.25 3.25"
						stroke="currentColor"
						strokeWidth="1.4"
						strokeLinecap="round"
					/>
				</svg>

				<input
					className={styles.searchInput}
					type="search"
					value={search}
					onChange={(event) => setSearch(event.target.value)}
					placeholder="Найти слово или перевод"
					aria-label="Поиск слов"
				/>

				{search && (
					<button
						type="button"
						className={styles.clearSearch}
						onClick={() => setSearch("")}
						aria-label="Очистить поиск">
						<svg width="9" height="9" viewBox="0 0 9 9" fill="none">
							<path
								d="M2 2l5 5M7 2L2 7"
								stroke="currentColor"
								strokeWidth="1.2"
								strokeLinecap="round"
							/>
						</svg>
					</button>
				)}
			</div>

			{/* LEVEL */}

			<div ref={levelRef} className={styles.dropdown}>
				<button
					type="button"
					className={`${styles.dropdownButton} ${
						levelMenuOpen || levelFilter !== "all" ?
							styles.dropdownButtonActive
						:	""
					}`}
					onClick={handleLevelMenu}>
					<span className={styles.dropdownContent}>
						<span
							className={`${styles.levelIcon} ${
								levelFilter === "all" ?
									styles.levelAll
								:	styles[`level${levelFilter}`]
							}`}>
							{levelFilter === "all" ? "LVL" : levelFilter}
						</span>

						<span className={styles.dropdownInformation}>
							<span className={styles.dropdownLabel}>Уровень</span>

							<span className={styles.dropdownValue}>
								{levelFilter === "all" ? "Любой" : levelFilter}
							</span>
						</span>
					</span>

					<ChevronIcon open={levelMenuOpen} />
				</button>

				{levelMenuOpen && (
					<div className={styles.levelDropdownMenu}>
						{LEVELS.map((level) => {
							const active = levelFilter === level;

							return (
								<button
									key={level}
									type="button"
									className={`${styles.levelOption} ${
										active ?
											styles[
												level === "all" ? "levelOptionAll" : (
													`levelOption${level}`
												)
											]
										:	""
									}`}
									onClick={() => handleLevelSelect(level)}>
									{level === "all" ? "Любой" : level}
								</button>
							);
						})}
					</div>
				)}
			</div>

			{/* STATUS */}

			<div ref={statusRef} className={styles.dropdown}>
				<button
					type="button"
					className={`${styles.dropdownButton} ${styles.statusDropdownButton} ${
						statusMenuOpen || statusFilter !== "all" ?
							styles.dropdownButtonActive
						:	""
					}`}
					onClick={handleStatusMenu}>
					<span className={styles.dropdownContent}>
						<span
							className={`${styles.statusDot} ${
								styles[`statusDot${capitalize(activeStatus.value)}`]
							}`}
						/>

						<span className={styles.dropdownInformation}>
							<span className={styles.dropdownLabel}>Коллекция</span>

							<span className={styles.dropdownValue}>{activeStatus.label}</span>
						</span>
					</span>

					<ChevronIcon open={statusMenuOpen} />
				</button>

				{statusMenuOpen && (
					<div className={styles.statusDropdownMenu}>
						{STATUS_OPTIONS.map((status) => {
							const active = statusFilter === status.value;

							return (
								<button
									key={status.value}
									type="button"
									className={`${styles.statusOption} ${
										active ? styles.statusOptionActive : ""
									}`}
									onClick={() => handleStatusSelect(status.value)}>
									<span className={styles.statusOptionLeft}>
										<span
											className={`${styles.statusOptionDot} ${
												styles[`statusDot${capitalize(status.value)}`]
											}`}
										/>

										{status.label}
									</span>

									{active && (
										<svg width="12" height="12" viewBox="0 0 12 12" fill="none">
											<path
												d="M2.5 6l2.2 2.2 4.8-5"
												stroke="currentColor"
												strokeWidth="1.4"
												strokeLinecap="round"
												strokeLinejoin="round"
											/>
										</svg>
									)}
								</button>
							);
						})}
					</div>
				)}
			</div>

			{/* RESET */}

			{filtersActive && (
				<button
					type="button"
					className={styles.resetButton}
					onClick={resetFilters}
					title="Сбросить фильтры"
					aria-label="Сбросить фильтры">
					<svg width="14" height="14" viewBox="0 0 14 14" fill="none">
						<path
							d="M3 3l8 8M11 3l-8 8"
							stroke="currentColor"
							strokeWidth="1.4"
							strokeLinecap="round"
						/>
					</svg>
				</button>
			)}

			{/* RESULT */}

			<div className={styles.result}>
				<div className={styles.progressCircle}>
					<svg width="30" height="30" viewBox="0 0 30 30">
						<circle
							className={styles.progressCircleBackground}
							cx="15"
							cy="15"
							r="11.5"
						/>

						<circle
							className={styles.progressCircleValue}
							cx="15"
							cy="15"
							r="11.5"
							strokeDasharray={circleLength}
							strokeDashoffset={circleOffset}
						/>
					</svg>

					<span>{totalStudied}</span>
				</div>

				<div className={styles.resultText}>
					<strong>{filteredWordsCount}</strong>

					<span>слов найдено</span>
				</div>
			</div>
		</div>
	);
};

const ChevronIcon = ({ open }: { open: boolean }) => {
	return (
		<svg
			className={`${styles.chevron} ${open ? styles.chevronOpen : ""}`}
			width="11"
			height="11"
			viewBox="0 0 11 11"
			fill="none">
			<path
				d="M2.5 4l3 3 3-3"
				stroke="currentColor"
				strokeWidth="1.3"
				strokeLinecap="round"
				strokeLinejoin="round"
			/>
		</svg>
	);
};

const capitalize = (value: string) =>
	value.charAt(0).toUpperCase() + value.slice(1);

export default Toolbar;
