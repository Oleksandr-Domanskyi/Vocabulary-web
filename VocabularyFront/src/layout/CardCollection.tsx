import WordCard, {
	type Word,
	type WordStatus,
} from "../components/WordCard/WordCard";

import styles from "./CardCollection.module.css";

interface CardCollectionProps {
	words: Word[];
	known: Set<number>;
	unknown: Set<number>;
	favorites: Set<number>;

	onOpenCard: (id: number) => void;
	onToggleFavorite: (id: number) => void;
}

const CardCollection = ({
	words,
	known,
	unknown,
	favorites,
	onOpenCard,
	onToggleFavorite,
}: CardCollectionProps) => {
	const getWordStatus = (id: number): WordStatus => {
		if (known.has(id)) {
			return "known";
		}

		if (unknown.has(id)) {
			return "unknown";
		}

		return "unseen";
	};

	if (words.length === 0) {
		return <div className={styles.empty}>Нет слов по выбранным фильтрам</div>;
	}

	return (
		<section className={styles.collection}>
			{words.map((word) => (
				<WordCard
					key={word.id}
					word={word}
					status={getWordStatus(word.id)}
					isFavorite={favorites.has(word.id)}
					onOpen={onOpenCard}
					onToggleFavorite={onToggleFavorite}
				/>
			))}
		</section>
	);
};

export default CardCollection;
