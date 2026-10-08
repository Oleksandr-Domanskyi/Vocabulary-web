import { ChartNoAxesColumnIncreasing, Trophy } from "lucide-react";
import FlameIcon from "../components/FlameIcon/FlameIcon";
import styles from "./Hero.module.css";

interface HeroProps {
	name: string;
	dayStreak?: number;
	wordsStudied?: number;
	level?: number;
}

const Hero = ({
	name,
	dayStreak = 5,
	wordsStudied = 25,
	level = 4,
}: HeroProps) => {
	return (
		<div className={styles.Hero}>
			<div className={styles.title}>
				<div className={styles.title}>
					<h1>Welcome, {name}! 👋</h1>
					<p>
						Every new word brings you one step closer to fluency. Keep building
						your vocabulary, challenge yourself, and turn small daily
						achievements into lasting progress. Your journey to mastering
						English continues today!
					</p>
				</div>
			</div>

			<div className={styles.statistics}>
				<div className={styles.dayStreak}>
					<FlameIcon size={30} className={styles.flameIcon} />
					<div className={styles.statText}>
						<h2>{dayStreak}</h2>
						<p>Day streak</p>
					</div>
				</div>

				<div className={styles.wordsStudied}>
					<ChartNoAxesColumnIncreasing
						className={styles.chartIcon}
						size={24}
						strokeWidth={3}
					/>
					<div className={styles.statText}>
						<h2>{wordsStudied}</h2>
						<p>Total words</p>
					</div>
				</div>

				<div className={styles.level}>
					<span className={styles.icon}>🏆</span>
					<div className={styles.statText}>
						<h2>{level}</h2>
						<p>Level</p>
					</div>
				</div>
			</div>
		</div>
	);
};

export default Hero;
