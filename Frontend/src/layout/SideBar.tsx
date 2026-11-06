import {
	LayoutGrid,
	Clock3,
	CircleCheck,
	Heart,
	Archive,
	ChevronRight,
} from "lucide-react";

import styles from "./SideBar.module.css";

export type SidebarPage =
	| "all"
	| "learning"
	| "known"
	| "favorites"
	| "archive";

interface SideBarProps {
	activePage: SidebarPage;
	totalWords?: number;
	learningWords?: number;
	knownWords?: number;
	favoriteWords?: number;
	archivedWords?: number;
	dailyProgress?: number;
	dailyGoal?: number;
	onNavigate: (page: SidebarPage) => void;
}

const SideBar = ({
	activePage,
	totalWords = 0,
	learningWords = 0,
	knownWords = 0,
	favoriteWords = 0,
	archivedWords = 0,
	dailyProgress = 0,
	dailyGoal = 10,
	onNavigate,
}: SideBarProps) => {
	const navigation = [
		{
			id: "all",
			label: "Все слова",
			icon: LayoutGrid,
			count: totalWords,
		},
		{
			id: "learning",
			label: "В процессе",
			icon: Clock3,
			count: learningWords,
		},
		{
			id: "known",
			label: "Изученные",
			icon: CircleCheck,
			count: knownWords,
		},
		{
			id: "favorites",
			label: "Избранные",
			icon: Heart,
			count: favoriteWords,
		},
		{
			id: "archive",
			label: "Архив",
			icon: Archive,
			count: archivedWords,
		},
	] as const;

	const progress = Math.min(
		100,
		Math.max(0, (dailyProgress / Math.max(1, dailyGoal)) * 100),
	);

	return (
		<aside className={styles.SideBar}>
			<nav className={styles.navigation}>
				{navigation.map((item) => {
					const Icon = item.icon;

					return (
						<button
							key={item.id}
							type="button"
							className={`${styles.navItem} ${
								activePage === item.id ? styles.active : ""
							}`}
							onClick={() => onNavigate(item.id)}>
							<Icon size={17} strokeWidth={1.8} className={styles.navIcon} />

							<span className={styles.navLabel}>{item.label}</span>

							<span className={styles.navCount}>{item.count}</span>
						</button>
					);
				})}
			</nav>

			<div className={styles.dailyCard}>
				<span className={styles.dailyTitle}>Сегодня</span>

				<div className={styles.progressContent}>
					<div
						className={styles.progressCircle}
						style={{
							background: `conic-gradient(
                                #8b5cf6 ${progress}%,
                                #29232f ${progress}% 100%
                            )`,
						}}>
						<div className={styles.progressInner} />
					</div>

					<div className={styles.progressInfo}>
						<strong>
							{dailyProgress} / {dailyGoal}
						</strong>
						<span>слов изучено</span>
					</div>
				</div>

				<button
					type="button"
					className={styles.continueButton}
					onClick={() => onNavigate("learning")}>
					Продолжить
					<ChevronRight size={15} />
				</button>
			</div>
		</aside>
	);
};

export default SideBar;
