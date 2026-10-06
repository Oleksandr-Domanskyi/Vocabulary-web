import styles from "./Account.module.css";
import accountImage from "../../assets/images/GlobalAccountImage.png";

const Account = () => {
	const level = 11;
	const streak = 7;

	const getLevelClass = (level: number) => {
		if (level >= 51) return styles.levelLegendary;
		if (level >= 31) return styles.levelGold;
		if (level >= 21) return styles.levelPurple;
		if (level >= 11) return styles.levelBlue;
		if (level >= 6) return styles.levelGreen;

		return styles.levelDefault;
	};
	return (
		<div className={styles.account}>
			<img className={styles.accountImage} src={accountImage} alt="Account" />

			<span className={styles.accountEmail}>your@email.com</span>

			<span className={`${styles.accountLevel} ${getLevelClass(level)}`}>
				LVL {level}
			</span>
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

				<span className={styles.streakValue}>7</span>
				<span className={styles.streakUnit}>days</span>
			</div>
		</div>
	);
};

export default Account;
