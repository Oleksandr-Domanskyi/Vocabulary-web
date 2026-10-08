import { AnimatePresence, LayoutGroup, motion } from "motion/react";

import WordCard, {
	type Word,
	type WordStatus,
} from "../components/WordCard/WordCard";

import WordListItem from "../components/WordListItem/WordListItem";

import styles from "./CardCollection.module.css";

export type ViewMode = "grid" | "list";

interface CardCollectionProps {
	words: Word[];
	known: Set<number>;
	unknown: Set<number>;
	favorites: Set<number>;
	viewMode: ViewMode;
	onOpenCard: (id: number) => void;
	onToggleFavorite: (id: number) => void;
}

const CardCollection = ({
	words,
	known,
	unknown,
	favorites,
	viewMode,
	onOpenCard,
	onToggleFavorite,
}: CardCollectionProps) => {
	const getWordStatus = (id: number): WordStatus => {
		if (known.has(id)) return "known";
		if (unknown.has(id)) return "unknown";
		return "unseen";
	};

	return (
		<LayoutGroup>
			<section
				className={
					viewMode === "list" ? styles.listCollection : styles.collection
				}
				style={{ position: "relative" }}>
				<AnimatePresence initial={false} mode="popLayout">
					{words.map((word) => {
						const commonProps = {
							word,
							status: getWordStatus(word.id),
							isFavorite: favorites.has(word.id),
							onOpen: onOpenCard,
							onToggleFavorite,
						};

						return (
							<motion.div
								key={word.id}
								layout="position"
								initial={{
									opacity: 0,
									scale: 0.94,
								}}
								animate={{
									opacity: 1,
									scale: 1,
								}}
								exit={{
									opacity: 0,
									scale: 0.94,
								}}
								transition={{
									layout: {
										type: "tween",
										duration: 0.42,
										ease: [0.22, 1, 0.36, 1],
									},
									opacity: {
										duration: 0.22,
									},
									scale: {
										duration: 0.28,
										ease: [0.22, 1, 0.36, 1],
									},
								}}
								style={{
									minWidth: 0,
								}}>
								{viewMode === "list" ?
									<WordListItem {...commonProps} />
								:	<WordCard {...commonProps} />}
							</motion.div>
						);
					})}
				</AnimatePresence>

				{words.length === 0 && (
					<motion.div
						className={styles.empty}
						initial={{ opacity: 0 }}
						animate={{ opacity: 1 }}
						transition={{ duration: 0.25 }}>
						Нет слов по выбранным фильтрам
					</motion.div>
				)}
			</section>
		</LayoutGroup>
	);
};

export default CardCollection;
