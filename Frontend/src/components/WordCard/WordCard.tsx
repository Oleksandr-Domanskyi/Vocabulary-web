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
					<svg
						width="18"
						height="18"
						viewBox="0 0 24 24"
						fill="none"
						xmlns="http://www.w3.org/2000/svg">
						<path
							d="M20.84 4.61a5.5 5.5 0 0 0-7.78 0L12 5.67l-1.06-1.06a5.5 5.5 0 0 0-7.78 7.78l1.06 1.06L12 21.23l7.78-7.78 1.06-1.06a5.5 5.5 0 0 0 0-7.78z"
							fill={isFavorite ? "currentColor" : "none"}
							stroke="currentColor"
							strokeWidth="1.8"
							strokeLinecap="round"
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
