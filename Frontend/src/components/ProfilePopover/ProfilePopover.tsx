import { useState } from "react";

import {
	Activity,
	BookOpen,
	Camera,
	CheckCircle2,
	ChevronLeft,
	ChevronRight,
	Flame,
	Heart,
	Pencil,
	Sparkles,
	X,
} from "lucide-react";

import accountImage from "../../assets/images/GlobalAccountImage.png";
import styles from "./ProfilePopover.module.css";

interface ProfilePopoverProps {
	nickname: string;
	email: string;
	level: number;
	currentXp: number;
	requiredXp: number;
	knownWords: number;
	learningWords: number;
	favoriteWords: number;
	streak: number;
	activity: Record<string, number>;
	onClose: () => void;
}

const WEEKDAYS = ["Пн", "Вт", "Ср", "Чт", "Пт", "Сб", "Вс"];

const MONTHS = [
	"Январь",
	"Февраль",
	"Март",
	"Апрель",
	"Май",
	"Июнь",
	"Июль",
	"Август",
	"Сентябрь",
	"Октябрь",
	"Ноябрь",
	"Декабрь",
];

const getDateKey = (date: Date): string => {
	const year = date.getFullYear();
	const month = String(date.getMonth() + 1).padStart(2, "0");
	const day = String(date.getDate()).padStart(2, "0");

	return `${year}-${month}-${day}`;
};

const getActivityLevel = (count: number): number => {
	if (count >= 10) return 4;
	if (count >= 6) return 3;
	if (count >= 3) return 2;
	if (count >= 1) return 1;

	return 0;
};

const ProfilePopover = ({
	nickname,
	email,
	level,
	currentXp,
	requiredXp,
	knownWords,
	learningWords,
	favoriteWords,
	streak,
	activity,
	onClose,
}: ProfilePopoverProps) => {
	const [isEditing, setIsEditing] = useState(false);

	const [displayedMonth, setDisplayedMonth] = useState(() => {
		const date = new Date();

		return new Date(date.getFullYear(), date.getMonth(), 1);
	});

	const today = new Date();

	const year = displayedMonth.getFullYear();
	const month = displayedMonth.getMonth();

	const firstDay = new Date(year, month, 1).getDay();
	const leadingDays = (firstDay + 6) % 7;

	const daysInMonth = new Date(year, month + 1, 0).getDate();

	const calendarDays: (number | null)[] = [
		...Array.from({ length: leadingDays }, () => null),
		...Array.from({ length: daysInMonth }, (_, index) => index + 1),
	];

	const xpProgress = Math.min(
		100,
		Math.max(0, (currentXp / Math.max(requiredXp, 1)) * 100),
	);

	const currentMonth =
		year === today.getFullYear() && month === today.getMonth();

	const monthActivity = Array.from({ length: daysInMonth }, (_, index) => {
		const date = new Date(year, month, index + 1);

		return activity[getDateKey(date)] ?? 0;
	});

	const activeDays = monthActivity.filter((count) => count > 0).length;

	const statistics = [
		{
			label: "Изучено",
			value: knownWords,
			icon: CheckCircle2,
			className: styles.statKnown,
		},
		{
			label: "В процессе",
			value: learningWords,
			icon: BookOpen,
			className: styles.statLearning,
		},
		{
			label: "Избранное",
			value: favoriteWords,
			icon: Heart,
			className: styles.statFavorite,
		},
		{
			label: "Серия дней",
			value: streak,
			icon: Flame,
			className: styles.statStreak,
		},
	];

	const changeMonth = (offset: number) => {
		setDisplayedMonth((current) => {
			return new Date(current.getFullYear(), current.getMonth() + offset, 1);
		});
	};

	const handleClose = () => {
		if (isEditing) {
			setIsEditing(false);
			return;
		}

		onClose();
	};

	return (
		<section className={styles.popover} aria-label="Профиль пользователя">
			{/* PROFILE */}

			<div className={styles.profile}>
				<div
					className={`${styles.avatarWrapper} ${
						isEditing ? styles.avatarEditable : ""
					}`}>
					<img
						className={styles.avatar}
						src={accountImage}
						alt="Аватар пользователя"
					/>

					{isEditing ?
						<span
							className={styles.avatarEditOverlay}
							title="Изменить фотографию">
							<Camera size={20} />
						</span>
					:	<span className={styles.avatarDecoration}>
							<Sparkles size={13} />
						</span>
					}
				</div>

				<div className={styles.profileInfo}>
					<div className={styles.nicknameRow}>
						{isEditing ?
							<div
								className={styles.editableField}
								title="Редактировать никнейм">
								<span>{nickname}</span>
								<Pencil size={13} />
							</div>
						:	<>
								<h2 className={styles.nickname}>{nickname}</h2>

								<button
									type="button"
									className={styles.editButton}
									title="Редактировать профиль"
									aria-label="Редактировать профиль"
									onClick={() => setIsEditing(true)}>
									<Pencil size={15} strokeWidth={1.8} />
								</button>
							</>
						}
					</div>

					{isEditing ?
						<div className={styles.editableField} title="Редактировать email">
							<span>{email}</span>
							<Pencil size={13} />
						</div>
					:	<span className={styles.email}>{email}</span>}

					<span className={styles.profileBadge}>
						<Sparkles size={12} />
						Уровень {level}
					</span>
				</div>

				<button
					type="button"
					className={styles.closeButton}
					onClick={handleClose}
					title={isEditing ? "Выйти из редактирования" : "Закрыть профиль"}
					aria-label={
						isEditing ? "Выйти из редактирования" : "Закрыть профиль"
					}>
					<X size={17} />
				</button>
			</div>

			<div className={styles.divider} />

			{/* EXPERIENCE */}

			<div className={styles.experience}>
				<div className={styles.sectionHeading}>
					<div className={styles.levelHeading}>
						<Sparkles size={16} />
						<span>Уровень {level}</span>
					</div>

					<span className={styles.xpValue}>
						{currentXp} / {requiredXp} XP
					</span>
				</div>

				<div
					className={styles.xpTrack}
					role="progressbar"
					aria-label="Прогресс уровня"
					aria-valuenow={Math.min(requiredXp, Math.max(0, currentXp))}
					aria-valuemin={0}
					aria-valuemax={requiredXp}>
					<div
						className={styles.xpFill}
						style={{
							width: `${xpProgress}%`,
						}}
					/>
				</div>

				<div className={styles.xpFooter}>
					<span>{Math.round(xpProgress)}% завершено</span>

					<span>
						{Math.max(0, requiredXp - currentXp)} XP до LVL {level + 1}
					</span>
				</div>
			</div>

			<div className={styles.divider} />

			{/* STATISTICS */}

			<div className={styles.statisticsSection}>
				<div className={styles.sectionTitle}>
					<Activity size={16} />
					<h3>Статистика</h3>
				</div>

				<div className={styles.statisticsGrid}>
					{statistics.map((stat) => {
						const Icon = stat.icon;

						return (
							<div
								key={stat.label}
								className={`${styles.statCard} ${stat.className}`}>
								<div className={styles.statHeading}>
									<Icon size={17} />
									<span>{stat.label}</span>
								</div>

								<strong className={styles.statValue}>{stat.value}</strong>
							</div>
						);
					})}
				</div>
			</div>

			<div className={styles.divider} />

			{/* CALENDAR */}

			<div className={styles.calendarSection}>
				<div className={styles.sectionTitle}>
					<Activity size={16} />
					<h3>Календарь активности</h3>
				</div>

				<div className={styles.calendarHeader}>
					<span className={styles.monthLabel}>
						{MONTHS[month]} {year}
					</span>

					<div className={styles.monthNavigation}>
						<button
							type="button"
							onClick={() => changeMonth(-1)}
							aria-label="Предыдущий месяц">
							<ChevronLeft size={17} />
						</button>

						<button
							type="button"
							onClick={() => changeMonth(1)}
							disabled={currentMonth}
							aria-label="Следующий месяц">
							<ChevronRight size={17} />
						</button>
					</div>
				</div>

				<div className={styles.calendarGrid}>
					{WEEKDAYS.map((day) => (
						<span key={day} className={styles.weekday}>
							{day}
						</span>
					))}

					{calendarDays.map((day, index) => {
						if (day === null) {
							return (
								<span key={`empty-${index}`} className={styles.emptyDay} />
							);
						}

						const date = new Date(year, month, day);

						const key = getDateKey(date);
						const count = activity[key] ?? 0;

						const intensity = getActivityLevel(count);

						const isToday = key === getDateKey(today);

						const isFuture =
							date.getTime() >
							new Date(
								today.getFullYear(),
								today.getMonth(),
								today.getDate(),
							).getTime();

						return (
							<div
								key={key}
								className={[
									styles.calendarDay,
									styles[`intensity${intensity}`],
									isToday ? styles.today : "",
									isFuture ? styles.futureDay : "",
								]
									.filter(Boolean)
									.join(" ")}
								title={`${day} ${MONTHS[month].toLowerCase()}: ${count} действий`}
								aria-label={`${day} ${MONTHS[month]}: ${count} действий`}>
								{day}
							</div>
						);
					})}
				</div>

				<div className={styles.calendarFooter}>
					<span>
						<Flame size={14} />
						{activeDays} активных дней
					</span>

					<div className={styles.legend}>
						<span>Меньше</span>

						{[0, 1, 2, 3, 4].map((value) => (
							<span
								key={value}
								className={`${styles.legendSquare} ${
									styles[`intensity${value}`]
								}`}
							/>
						))}

						<span>Больше</span>
					</div>
				</div>
			</div>
		</section>
	);
};

export default ProfilePopover;
