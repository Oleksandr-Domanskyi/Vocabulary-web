import type { MouseEvent } from "react";
import { Heart, ChevronRight } from "lucide-react";

import type { Word, WordStatus } from "../WordCard/WordCard";

import styles from "./WordListItem.module.css";

interface WordListItemProps {
	word: Word;
	status: WordStatus;
	isFavorite: boolean;

	onOpen: (id: number) => void;
	onToggleFavorite: (id: number) => void;
}

const STATUS_LABELS: Record<WordStatus, string> = {
	known: "Знаю",
	unknown: "Учу",
	unseen: "Новое",
};

const WordListItem = ({
	word,
	status,
	isFavorite,
	onOpen,
	onToggleFavorite,
}: WordListItemProps) => {
	const handleFavoriteClick = (event: MouseEvent<HTMLButtonElement>) => {
		event.stopPropagation();
		onToggleFavorite(word.id);
	};

	return (
		<article className={styles.item} onClick={() => onOpen(word.id)}>
			<div className={styles.imageContainer}>
				<img
					className={styles.image}
					src={word.image}
					alt={word.word}
					loading="lazy"
				/>
			</div>

			<div className={styles.wordInfo}>
				<div className={styles.wordHeading}>
					<h3 className={styles.word}>{word.word}</h3>

					<span className={styles.partOfSpeech}>{word.partOfSpeech}</span>
				</div>

				<span className={styles.transcription}>{word.transcription}</span>
			</div>

			<div className={styles.translation}>{word.translation}</div>

			<span className={`${styles.level} ${styles[`level${word.level}`]}`}>
				{word.level}
			</span>

			<span className={`${styles.status} ${styles[`status${status}`]}`}>
				<span className={styles.statusDot} />
				{STATUS_LABELS[status]}
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
				<Heart
					size={18}
					strokeWidth={1.8}
					fill={isFavorite ? "currentColor" : "none"}
				/>
			</button>

			<ChevronRight className={styles.chevron} size={18} strokeWidth={1.7} />
		</article>
	);
};

export default WordListItem;
