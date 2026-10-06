import {
    useEffect,
    useState,
    type AnimationEvent,
    type MouseEvent,
} from "react";

import type {
    Word,
    WordStatus,
} from "../WordCard/WordCard";

import styles from "./WordModal.module.css";

type SlideDirection = "left" | "right";

interface WordModalProps {
    word: Word;
    status: WordStatus;
    isFavorite: boolean;
    isArchived?: boolean;
    slideDirection: SlideDirection;
    onClose: () => void;
    onToggleFavorite: (id: number) => void;
    onStatusChange: (id: number, status: WordStatus) => void;
    onPrevious?: () => void;
    onNext?: () => void;
    hasPrevious?: boolean;
    hasNext?: boolean;
    onArchive?: (id: number) => void;
    onDismiss?: (id: number) => void;
}

interface ExampleProps {
    index: string;
    text: string;
    translation: string;
    word: string;
    primary?: boolean;
}

const statusLabels: Record<WordStatus, string> = {
    known: "знаю",
    unknown: "учу",
    unseen: "новое",
};

const WordModal = ({
    word,
    status,
    isFavorite,
    isArchived = false,
    slideDirection,
    onClose,
    onToggleFavorite,
    onStatusChange,
    onPrevious,
    onNext,
    hasPrevious = false,
    hasNext = false,
    onArchive,
    onDismiss,
}: WordModalProps) => {
    const [flipped, setFlipped] = useState(false);
    const [speaking, setSpeaking] = useState(false);
    const [closing, setClosing] = useState(false);

    useEffect(() => {
        setFlipped(false);
        setSpeaking(false);
        window.speechSynthesis?.cancel();
    }, [word.id]);

    useEffect(() => {
        const handleKeyDown = (event: KeyboardEvent) => {
            if (event.key === "Escape") {
                if (!closing) {
                    window.speechSynthesis?.cancel();
                    setClosing(true);
                }

                return;
            }

            if (closing) {
                return;
            }

            if (event.key === "ArrowLeft" && hasPrevious) {
                event.preventDefault();
                onPrevious?.();
                return;
            }

            if (event.key === "ArrowRight" && hasNext) {
                event.preventDefault();
                onNext?.();
            }
        };

        window.addEventListener("keydown", handleKeyDown);

        return () => {
            window.removeEventListener("keydown", handleKeyDown);
        };
    }, [
        closing,
        hasPrevious,
        hasNext,
        onPrevious,
        onNext,
    ]);

    useEffect(() => {
        return () => {
            window.speechSynthesis?.cancel();
        };
    }, []);

    const closeModal = () => {
        if (closing) {
            return;
        }

        window.speechSynthesis?.cancel();
        setClosing(true);
    };

    const handleOverlayAnimationEnd = (
        event: AnimationEvent<HTMLDivElement>,
    ) => {
        if (
            closing &&
            event.target === event.currentTarget &&
            event.animationName.includes("overlayClose")
        ) {
            onClose();
        }
    };

    const handleOverlayClick = (
        event: MouseEvent<HTMLDivElement>,
    ) => {
        if (event.target === event.currentTarget) {
            closeModal();
        }
    };

    const handleSpeak = () => {
        if (!("speechSynthesis" in window)) {
            return;
        }

        window.speechSynthesis.cancel();

        const utterance = new SpeechSynthesisUtterance(word.word);

        utterance.lang = "en-US";
        utterance.rate = 0.85;
        utterance.onstart = () => setSpeaking(true);
        utterance.onend = () => setSpeaking(false);
        utterance.onerror = () => setSpeaking(false);

        window.speechSynthesis.speak(utterance);
    };

    return (
        <div
            className={`${styles.overlay} ${
                closing ? styles.overlayClosing : ""
            }`}
            onMouseDown={handleOverlayClick}
            onAnimationEnd={handleOverlayAnimationEnd}
        >
            <div
                className={`${styles.modalContainer} ${
                    closing
                        ? styles.modalClosing
                        : styles.modalOpening
                }`}
            >
                <div className={styles.closeRow}>
                    <button
                        type="button"
                        className={styles.closeButton}
                        onClick={closeModal}
                    >
                        <kbd>Esc</kbd>
                        <span>закрыть</span>
                    </button>
                </div>

                {hasPrevious && !closing && (
                    <button
                        type="button"
                        className={`${styles.navigationButton} ${styles.previousButton}`}
                        onClick={onPrevious}
                        aria-label="Предыдущее слово"
                    >
                        <ArrowLeftIcon />
                    </button>
                )}

                {hasNext && !closing && (
                    <button
                        type="button"
                        className={`${styles.navigationButton} ${styles.nextButton}`}
                        onClick={onNext}
                        aria-label="Следующее слово"
                    >
                        <ArrowRightIcon />
                    </button>
                )}

                <div
                    key={word.id}
                    className={`${styles.cardScene} ${
                        slideDirection === "left"
                            ? styles.slideFromRight
                            : styles.slideFromLeft
                    }`}
                    onMouseDown={(event) => event.stopPropagation()}
                >
                    <div
                        className={`${styles.card3d} ${
                            flipped ? styles.flipped : ""
                        }`}
                    >
                        <section
                            className={`${styles.cardFace} ${styles.cardFront}`}
                        >
                            <div className={styles.hero}>
                                <img
                                    src={word.image}
                                    alt={word.word}
                                    className={styles.heroImage}
                                />
                                <div className={styles.heroOverlay} />
                                <span
                                    className={`${styles.level} ${
                                        styles[`level${word.level}`]
                                    }`}
                                >
                                    {word.level}
                                </span>
                                <div
                                    className={`${styles.status} ${
                                        styles[`status${status}`]
                                    }`}
                                >
                                    <span className={styles.statusDot} />
                                    <span>{statusLabels[status]}</span>
                                </div>
                            </div>

                            <div className={styles.actionBar}>
                                <button
                                    type="button"
                                    className={`${styles.likeButton} ${
                                        isFavorite
                                            ? styles.likeButtonActive
                                            : ""
                                    }`}
                                    onClick={() =>
                                        onToggleFavorite(word.id)
                                    }
                                >
                                    <span
                                        className={
                                            styles.likeIconContainer
                                        }
                                    >
                                        <HeartIcon filled={isFavorite} />
                                    </span>
                                    <span className={styles.likeText}>
                                        <strong>
                                            {isFavorite
                                                ? "Вам нравится"
                                                : "Нравится"}
                                        </strong>
                                        <small>
                                            Добавить слово в избранное
                                        </small>
                                    </span>
                                </button>

                                <div className={styles.secondaryActions}>
                                    {onArchive && (
                                        <button
                                            type="button"
                                            className={`${styles.smallAction} ${
                                                isArchived
                                                    ? styles.archiveActive
                                                    : ""
                                            }`}
                                            onClick={() =>
                                                onArchive(word.id)
                                            }
                                        >
                                            <span
                                                className={
                                                    styles.smallActionIcon
                                                }
                                            >
                                                <ArchiveIcon />
                                            </span>
                                            {isArchived
                                                ? "Вернуть"
                                                : "В архив"}
                                        </button>
                                    )}

                                    {onDismiss && (
                                        <button
                                            type="button"
                                            className={`${styles.smallAction} ${styles.dismissAction}`}
                                            onClick={() =>
                                                onDismiss(word.id)
                                            }
                                        >
                                            <span
                                                className={
                                                    styles.smallActionIcon
                                                }
                                            >
                                                <CloseIcon />
                                            </span>
                                            Не интересует
                                        </button>
                                    )}
                                </div>
                            </div>

                            <div className={styles.frontContent}>
                                <div className={styles.wordHeader}>
                                    <h2>{word.word}</h2>
                                    <span className={styles.partOfSpeech}>
                                        {word.partOfSpeech}
                                    </span>
                                </div>

                                <div className={styles.transcriptionRow}>
                                    <span className={styles.transcription}>
                                        {word.transcription}
                                    </span>
                                    <button
                                        type="button"
                                        className={`${styles.speakButton} ${
                                            speaking
                                                ? styles.speakButtonActive
                                                : ""
                                        }`}
                                        onClick={handleSpeak}
                                    >
                                        <SpeakerIcon />
                                        {speaking
                                            ? "играет..."
                                            : "слушать"}
                                    </button>
                                </div>

                                <div className={styles.meaning}>
                                    <span className={styles.sectionLabel}>
                                        значение
                                    </span>
                                    <p className={styles.translation}>
                                        {word.translation}
                                    </p>
                                    <div className={styles.mainExample}>
                                        <p>“{word.example}”</p>
                                        <span>
                                            {word.exampleTranslation}
                                        </span>
                                    </div>
                                </div>

                                <div className={styles.memorySection}>
                                    <div>
                                        <span
                                            className={
                                                styles.sectionLabel
                                            }
                                        >
                                            проверка памяти
                                        </span>
                                        <p>Как ощущается слово?</p>
                                    </div>

                                    <div className={styles.memoryActions}>
                                        <button
                                            type="button"
                                            className={`${styles.memoryButton} ${
                                                status === "unknown"
                                                    ? styles.unknownActive
                                                    : ""
                                            }`}
                                            onClick={() =>
                                                onStatusChange(
                                                    word.id,
                                                    "unknown",
                                                )
                                            }
                                        >
                                            Не знаю
                                        </button>
                                        <button
                                            type="button"
                                            className={`${styles.memoryButton} ${
                                                status === "known"
                                                    ? styles.knownActive
                                                    : ""
                                            }`}
                                            onClick={() =>
                                                onStatusChange(
                                                    word.id,
                                                    "known",
                                                )
                                            }
                                        >
                                            Знаю
                                        </button>
                                    </div>
                                </div>

                                <button
                                    type="button"
                                    className={styles.flipButton}
                                    onClick={() => setFlipped(true)}
                                >
                                    <FlipIcon />
                                    Пример и подробности
                                </button>
                            </div>
                        </section>

                        <section
                            className={`${styles.cardFace} ${styles.cardBack}`}
                        >
                            <div className={styles.backHeader}>
                                <div
                                    className={
                                        styles.backWordInformation
                                    }
                                >
                                    <strong>{word.word}</strong>
                                    <span>{word.transcription}</span>
                                    <button
                                        type="button"
                                        className={
                                            styles.backSpeakButton
                                        }
                                        onClick={handleSpeak}
                                        aria-label="Прослушать"
                                    >
                                        <SpeakerIcon />
                                    </button>
                                </div>

                                <button
                                    type="button"
                                    className={styles.backButton}
                                    onClick={() => setFlipped(false)}
                                >
                                    ← назад
                                </button>
                            </div>

                            <div className={styles.backContent}>
                                <div className={styles.backTranslation}>
                                    <span
                                        className={
                                            styles.sectionLabelPurple
                                        }
                                    >
                                        перевод
                                    </span>
                                    <p>{word.translation}</p>
                                </div>

                                <div className={styles.examplesSection}>
                                    <span className={styles.sectionLabel}>
                                        примеры употребления
                                    </span>

                                    <Example
                                        index="01"
                                        text={word.example}
                                        translation={
                                            word.exampleTranslation
                                        }
                                        word={word.word}
                                        primary
                                    />

                                    <Example
                                        index="02"
                                        text={`I often use the word ${word.word} when speaking English.`}
                                        translation={`Я часто использую слово ${word.translation} в английской речи.`}
                                        word={word.word}
                                    />

                                    <Example
                                        index="03"
                                        text={`This is another example with the word ${word.word}.`}
                                        translation={`Это ещё один пример со словом ${word.translation}.`}
                                        word={word.word}
                                    />
                                </div>

                                <div className={styles.sourceSection}>
                                    <span className={styles.sectionLabel}>
                                        источник
                                    </span>
                                    <a
                                        href={word.dictionaryUrl}
                                        target="_blank"
                                        rel="noopener noreferrer"
                                        className={styles.dictionaryLink}
                                    >
                                        <span
                                            className={
                                                styles.dictionaryIcon
                                            }
                                        >
                                            <DictionaryIcon />
                                        </span>
                                        <span>
                                            <strong>
                                                Cambridge Dictionary
                                            </strong>
                                            <small>
                                                Определение,
                                                произношение и полная
                                                статья
                                            </small>
                                        </span>
                                    </a>
                                </div>

                                <button
                                    type="button"
                                    className={`${styles.flipButton} ${styles.flipBackButton}`}
                                    onClick={() => setFlipped(false)}
                                >
                                    <FlipBackIcon />
                                    Перевернуть на лицевую сторону
                                </button>
                            </div>
                        </section>
                    </div>
                </div>
            </div>
        </div>
    );
};

const Example = ({
    index,
    text,
    translation,
    word,
    primary = false,
}: ExampleProps) => {
    const escapedWord = word.replace(
        /[.*+?^${}()|[\]\\]/g,
        "\\$&",
    );

    const parts = text.split(
        new RegExp(`(${escapedWord})`, "gi"),
    );

    return (
        <div className={styles.example}>
            <span
                className={`${styles.exampleNumber} ${
                    primary
                        ? styles.exampleNumberPrimary
                        : ""
                }`}
            >
                {index}
            </span>

            <div className={styles.exampleContent}>
                <p className={styles.exampleText}>
                    “
                    {parts.map((part, index) =>
                        part.toLowerCase() ===
                        word.toLowerCase() ? (
                            <strong
                                key={index}
                                className={
                                    styles.highlightedWord
                                }
                            >
                                {part}
                            </strong>
                        ) : (
                            <span key={index}>{part}</span>
                        ),
                    )}
                    ”
                </p>

                <span className={styles.exampleTranslation}>
                    {translation}
                </span>
            </div>
        </div>
    );
};

const ArrowLeftIcon = () => (
    <svg
        width="17"
        height="17"
        viewBox="0 0 17 17"
        fill="none"
    >
        <path
            d="M10.5 3.5l-5 5 5 5M6 8.5h7"
            stroke="currentColor"
            strokeWidth="1.5"
            strokeLinecap="round"
            strokeLinejoin="round"
        />
    </svg>
);

const ArrowRightIcon = () => (
    <svg
        width="17"
        height="17"
        viewBox="0 0 17 17"
        fill="none"
    >
        <path
            d="M6.5 3.5l5 5-5 5M11 8.5H4"
            stroke="currentColor"
            strokeWidth="1.5"
            strokeLinecap="round"
            strokeLinejoin="round"
        />
    </svg>
);

const HeartIcon = ({ filled }: { filled: boolean }) => (
    <svg
        width="16"
        height="16"
        viewBox="0 0 15 15"
        fill="none"
    >
        <path
            d="M7.5 13s-6-3.75-6-7.5a3.5 3.5 0 017 0 3.5 3.5 0 017 0c0 3.75-6 7.5-6 7.5z"
            fill={filled ? "currentColor" : "none"}
            stroke="currentColor"
            strokeWidth="1.4"
            strokeLinejoin="round"
        />
    </svg>
);

const SpeakerIcon = () => (
    <svg
        width="13"
        height="13"
        viewBox="0 0 14 14"
        fill="none"
    >
        <path
            d="M2 5.5h2.2L7 3v8L4.2 8.5H2v-3z"
            stroke="currentColor"
            strokeWidth="1.2"
            strokeLinejoin="round"
        />
        <path
            d="M9 5c.7.5 1 1.2 1 2s-.3 1.5-1 2M10.5 3.5c1.1.9 1.7 2 1.7 3.5s-.6 2.6-1.7 3.5"
            stroke="currentColor"
            strokeWidth="1.2"
            strokeLinecap="round"
        />
    </svg>
);

const ArchiveIcon = () => (
    <svg
        width="13"
        height="13"
        viewBox="0 0 13 13"
        fill="none"
    >
        <path
            d="M2 4h9v7H2V4zM1.5 2h10v2h-10V2zM5 6.5h3"
            stroke="currentColor"
            strokeWidth="1.15"
            strokeLinecap="round"
            strokeLinejoin="round"
        />
    </svg>
);

const CloseIcon = () => (
    <svg
        width="12"
        height="12"
        viewBox="0 0 12 12"
        fill="none"
    >
        <path
            d="M2.5 2.5l7 7M9.5 2.5l-7 7"
            stroke="currentColor"
            strokeWidth="1.2"
            strokeLinecap="round"
        />
    </svg>
);

const FlipIcon = () => (
    <svg
        width="14"
        height="14"
        viewBox="0 0 14 14"
        fill="none"
    >
        <path
            d="M2.5 5.25A4.75 4.75 0 0111 4M11 4V1.75M11 4H8.75M11.5 8.75A4.75 4.75 0 013 10M3 10v2.25M3 10h2.25"
            stroke="currentColor"
            strokeWidth="1.3"
            strokeLinecap="round"
            strokeLinejoin="round"
        />
    </svg>
);

const FlipBackIcon = () => (
    <svg
        width="14"
        height="14"
        viewBox="0 0 14 14"
        fill="none"
    >
        <path
            d="M3 4.5h6.5a2.5 2.5 0 010 5H8"
            stroke="currentColor"
            strokeWidth="1.4"
            strokeLinecap="round"
        />
        <path
            d="M5 2.5l-2 2 2 2M10 7.5l2 2-2 2"
            stroke="currentColor"
            strokeWidth="1.4"
            strokeLinecap="round"
            strokeLinejoin="round"
        />
    </svg>
);

const DictionaryIcon = () => (
    <svg
        width="13"
        height="13"
        viewBox="0 0 13 13"
        fill="none"
    >
        <rect
            x=".75"
            y=".75"
            width="11.5"
            height="11.5"
            rx="2.5"
            stroke="currentColor"
            strokeWidth="1.2"
        />
        <path
            d="M4 6.5h5M6.5 4l2.5 2.5L6.5 9"
            stroke="currentColor"
            strokeWidth="1.2"
            strokeLinecap="round"
            strokeLinejoin="round"
        />
    </svg>
);

export default WordModal;