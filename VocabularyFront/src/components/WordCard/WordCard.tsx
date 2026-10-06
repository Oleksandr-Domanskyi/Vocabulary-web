import type { MouseEvent } from "react";

import styles from "./WordCard.module.css";

export type WordLevel = "A2" | "B1" | "B2" | "C1";
export type WordStatus = "known" | "unknown" | "unseen";

export interface Word {
	id: number;
	word: string;
	transcription: string;
	translation: string;
	partOfSpeech: string;
	example: string;
	exampleTranslation: string;
	level: WordLevel;
	image: string;
	dictionaryUrl: string;
}

interface WordCardProps {
	word: Word;
	status: WordStatus;
	isFavorite: boolean;

	onOpen: (id: number) => void;
	onToggleFavorite: (id: number) => void;
}

const WordCard = ({
	word,
	status,
	isFavorite,
	onOpen,
	onToggleFavorite,
}: WordCardProps) => {
	const handleFavoriteClick = (event: MouseEvent<HTMLButtonElement>) => {
		event.stopPropagation();
		onToggleFavorite(word.id);
	};

	const statusLabel: Record<WordStatus, string> = {
		known: "знаю",
		unknown: "учу",
		unseen: "новое",
	};

	return (
		<article className={styles.card} onClick={() => onOpen(word.id)}>
			<div className={styles.imageContainer}>
				<img className={styles.image} src={word.image} alt={word.word} />

				<div className={styles.imageOverlay} />

				<span className={`${styles.level} ${styles[`level${word.level}`]}`}>
					{word.level}
				</span>

				<button
					type="button"
					className={`${styles.favoriteButton} ${
						isFavorite ? styles.favoriteActive : ""
					}`}
					onClick={handleFavoriteClick}
					aria-label={
						isFavorite ? "Удалить из избранного" : "Добавить в избранное"
					}>
					<svg width="15" height="15" viewBox="0 0 15 15" fill="none">
						<path
							d="M7.5 13s-6-3.75-6-7.5a3.5 3.5 0 017 0 3.5 3.5 0 017 0c0 3.75-6 7.5-6 7.5z"
							fill={isFavorite ? "currentColor" : "none"}
							stroke="currentColor"
							strokeWidth="1.4"
							strokeLinejoin="round"
						/>
					</svg>
				</button>

				<div className={`${styles.status} ${styles[`status${status}`]}`}>
					<span className={styles.statusDot} />
					<span>{statusLabel[status]}</span>
				</div>
			</div>

			<div className={styles.content}>
				<div className={styles.titleRow}>
					<h3 className={styles.word}>{word.word}</h3>

					<span className={styles.partOfSpeech}>{word.partOfSpeech}</span>
				</div>

				<span className={styles.transcription}>{word.transcription}</span>

				<span className={styles.translation}>{word.translation}</span>
			</div>
		</article>
	);
};

export default WordCard;
