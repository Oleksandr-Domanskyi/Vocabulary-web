import { useEffect, useRef, useState } from "react";

import ProfilePopover from "../ProfilePopover/ProfilePopover";

import styles from "./Account.module.css";
import accountImage from "../../assets/images/GlobalAccountImage.png";

interface AccountProps {
	knownWords?: number;
	learningWords?: number;
	favoriteWords?: number;
}

const Account = ({
	knownWords = 0,
	learningWords = 0,
	favoriteWords = 0,
}: AccountProps) => {
	const [isOpen, setIsOpen] = useState(false);
	const accountRef = useRef<HTMLDivElement>(null);

	const level = 11;
	const streak = 7;

	const currentXp = 720;
	const requiredXp = 1000;

	const nickname = "Sasha";
	const email = "your@email.com";

	// Demonstration data. Replace with actual activity history.
	const activity: Record<string, number> = {};

	const today = new Date();

	for (let offset = 0; offset < 14; offset++) {
		const date = new Date(
			today.getFullYear(),
			today.getMonth(),
			today.getDate() - offset,
		);

		const key = [
			date.getFullYear(),
			String(date.getMonth() + 1).padStart(2, "0"),
			String(date.getDate()).padStart(2, "0"),
		].join("-");

		activity[key] =
			offset % 4 === 0 ? 10
			: offset % 3 === 0 ? 5
			: 2;
	}

	const getLevelClass = (value: number) => {
		if (value >= 51) return styles.levelLegendary;
		if (value >= 31) return styles.levelGold;
		if (value >= 21) return styles.levelPurple;
		if (value >= 11) return styles.levelBlue;
		if (value >= 6) return styles.levelGreen;

		return styles.levelDefault;
	};

	useEffect(() => {
		if (!isOpen) return;

		const handleOutsideClick = (event: MouseEvent) => {
			if (
				accountRef.current &&
				!accountRef.current.contains(event.target as Node)
			) {
				setIsOpen(false);
			}
		};

		const handleEscape = (event: KeyboardEvent) => {
			if (event.key === "Escape") {
				setIsOpen(false);
			}
		};

		document.addEventListener("mousedown", handleOutsideClick);
		document.addEventListener("keydown", handleEscape);

		return () => {
			document.removeEventListener("mousedown", handleOutsideClick);
			document.removeEventListener("keydown", handleEscape);
		};
	}, [isOpen]);

	return (
		<div ref={accountRef} className={styles.accountWrapper}>
			<button
				type="button"
				className={styles.account}
				onClick={() => setIsOpen((current) => !current)}
				aria-expanded={isOpen}
				aria-haspopup="dialog"
				aria-label="Открыть профиль">
				<div className={styles.dailyStreak}>
					<svg
						className={styles.streakIcon}
						viewBox="0 0 24 24"
						aria-hidden="true">
						<path
							d="M13.2 2.5
                               C13.8 6.2 18.3 7.8 18.3 13
                               C18.3 17.2 15.5 20.5 11.8 20.5
                               C8.1 20.5 5.5 17.7 5.5 14.1
                               C5.5 11.3 7 9.2 9.1 7.3
                               C9 9.6 10 10.7 11 11.4
                               C11.2 7.9 12.7 5.7 13.2 2.5Z"
							fill="currentColor"
						/>

						<path
							d="M12.1 12.1
                               C12.5 14.1 14.5 15 14.5 17
                               C14.5 18.8 13.3 20.1 11.8 20.1
                               C10.2 20.1 9.1 18.9 9.1 17.4
                               C9.1 15.8 10.3 14.5 12.1 12.1Z"
							className={styles.streakInner}
						/>
					</svg>

					<span className={styles.streakValue}>{streak}</span>

					<span className={styles.streakUnit}>days</span>
				</div>

				<span className={styles.accountDivider} aria-hidden="true" />

				<img className={styles.accountImage} src={accountImage} alt="" />

				<span className={styles.accountEmail}>{email}</span>

				<span className={`${styles.accountLevel} ${getLevelClass(level)}`}>
					LVL {level}
				</span>
			</button>

			{isOpen && (
				<ProfilePopover
					nickname={nickname}
					email={email}
					level={level}
					currentXp={currentXp}
					requiredXp={requiredXp}
					knownWords={knownWords}
					learningWords={learningWords}
					favoriteWords={favoriteWords}
					streak={streak}
					activity={activity}
					onClose={() => setIsOpen(false)}
				/>
			)}
		</div>
	);
};

export default Account;
