import styles from "./StatisticsMenu.module.css";
import { useEffect, useRef, useState } from "react";

const StatisticsMenu = () => {
	const [favorites] = useState<Set<number>>(new Set());
	const [isStatisticsOpen, setIsStatisticsOpen] = useState(false);
	const statisticsRef = useRef<HTMLDivElement>(null);

	useEffect(() => {
		const handleClickOutside = (event: MouseEvent) => {
			if (
				statisticsRef.current &&
				!statisticsRef.current.contains(event.target as Node)
			) {
				setIsStatisticsOpen(false);
			}
		};

		document.addEventListener("mousedown", handleClickOutside);

		return () => {
			document.removeEventListener("mousedown", handleClickOutside);
		};
	}, []);
	return (
		<div className={styles.headerInfo}>
			<div className={styles.statisticsMenu} ref={statisticsRef}>
				<button
					className={`${styles.trophyButton} ${
						isStatisticsOpen ? styles.trophyButtonActive : ""
					}`}
					onClick={() => setIsStatisticsOpen(!isStatisticsOpen)}
					aria-label="Open statistics">
					<svg width="35" height="35" viewBox="0 0 24 24" fill="none">
						<path
							d="M8 21H16M12 17V21M7 4H17V8C17 12 14.8 15 12 15C9.2 15 7 12 7 8V4Z"
							stroke="currentColor"
							strokeWidth="1.7"
							strokeLinecap="round"
							strokeLinejoin="round"
						/>
						<path
							d="M7 6H4V8C4 10.2 5.8 12 8 12M17 6H20V8C20 10.2 18.2 12 16 12"
							stroke="currentColor"
							strokeWidth="1.7"
							strokeLinecap="round"
							strokeLinejoin="round"
						/>
					</svg>
				</button>

				{isStatisticsOpen && (
					<div
						className={`${styles.statisticsDropdown} ${
							isStatisticsOpen ? styles.dropdownOpen : styles.dropdownClosed
						}`}>
						<h3 className={styles.statisticsTitle}>Мой прогресс</h3>

						<div className={styles.statisticsRow}>
							<div className={styles.statisticsName}>
								<span
									className={`${styles.statisticsDot} ${styles.knownDot}`}
								/>
								Знаю
							</div>

							<span className={styles.knownValue}>0</span>
						</div>

						<div className={styles.statisticsRow}>
							<div className={styles.statisticsName}>
								<span
									className={`${styles.statisticsDot} ${styles.learningDot}`}
								/>
								Учу
							</div>

							<span className={styles.learningValue}>0</span>
						</div>

						<div className={styles.statisticsRow}>
							<div className={styles.statisticsName}>
								<span
									className={`${styles.statisticsDot} ${styles.likesDot}`}
								/>
								Лайки
							</div>

							<span className={styles.likesValue}>{favorites.size}</span>
						</div>
					</div>
				)}
			</div>
			<button className={styles.profile} aria-label="Open profile">
				<svg width="30" height="30" viewBox="0 0 24 24" fill="none">
					<path
						d="M20 21a8 8 0 0 0-16 0"
						stroke="currentColor"
						strokeWidth="1.7"
						strokeLinecap="round"
					/>
					<circle
						cx="12"
						cy="7"
						r="4"
						stroke="currentColor"
						strokeWidth="1.7"
					/>
				</svg>
			</button>
		</div>
	);
};
export default StatisticsMenu;
