
# Vocabulary

🚧 **Under Active Development**

Vocabulary is a personal web application designed to make learning English words and expressions easier, more convenient, and accessible.

The project focuses on building a personalized vocabulary based on words and expressions encountered in everyday life, rather than relying entirely on predefined datasets.

The main goal is to combine interactive flashcards, vocabulary management, contextual translation, and AI-powered learning tools into a single application.

## Application Preview

### Vocabulary Dashboard — Grid View

<p align="center">
  <img
    src="https://github.com/user-attachments/assets/a9a55102-3ba9-45ad-a4b8-38545912b0e6"
    alt="Vocabulary Dashboard Grid View"
    width="850"
  />
</p>

<p align="center">
  <em>Main dashboard with interactive vocabulary cards, search, filters, and learning statistics.</em>
</p>

### Vocabulary Dashboard — List View

<p align="center">
  <img
    src="https://github.com/user-attachments/assets/dbb49ba9-e135-47db-9c69-851d61c12a5c"
    alt="Vocabulary Dashboard List View"
    width="850"
  />
</p>

<p align="center">
  <em>Alternative list layout for browsing and managing vocabulary.</em>
</p>


### Interactive Flashcards


<table align="center">
  <tr>
    <th align="center" width="50%">Front Side</th>
    <th align="center" width="50%">Back Side</th>
  </tr>

  <tr>
    <td align="center" valign="top">
      <img
        src="https://github.com/user-attachments/assets/a7c489db-fa96-4f1c-aded-e793a8c74934"
        alt="Flashcard Front"
        width="350"
      />
    </td>
    <td align="center" valign="top">
      <img
        src="https://github.com/user-attachments/assets/8e4a6597-abdb-4305-a410-b25ff3caaa72"
        alt="Flashcard Back"
        width="350"
      />
    </td>
  </tr>
</table>

<p align="center">
  <em>Interactive flashcards displaying vocabulary information, translations, and usage examples.</em>
</p>




## Features

The current version focuses on frontend development and user interface functionality.

### Vocabulary Management

- Interactive vocabulary cards.
- Grid and list viewing modes.
- Search for words and expressions.
- Filtering by CEFR level (A2, B1, B2, C1).
- Filtering by learning status.
- Marking words as known or currently being learned.
- Adding and removing words from favorites.
- Archiving vocabulary cards.
- Detailed word information in an interactive modal.
- Navigation between vocabulary cards.

### User Interface

- Modern dark-themed design.
- Responsive layouts for different screen sizes.
- Smooth transitions between grid and list modes.
- Animated filtering and card rearrangement using Motion.
- Interactive buttons and hover effects.
- Sidebar navigation between vocabulary categories.
- Profile popover with user information.
- Learning statistics and activity calendar.
- Profile editing interface.

Some features currently use mock data and will be connected to the backend in future development stages.


## Planned Features

### Backend and Database

- ASP.NET Core REST API.
- PostgreSQL database integration.
- Entity Framework Core for data access.
- User registration and authentication.
- Persistent storage of vocabulary cards.
- Synchronization of learning progress across devices.

### AI-Powered Flashcards

- Automatic generation of vocabulary cards using AI.
- Context-aware translations and definitions.
- Generation of example sentences.
- Automatic identification of word levels and parts of speech.
- Support for individual words, expressions, and phrasal verbs.
- Validation of AI-generated card information.

### Browser Extension

A browser extension is planned to simplify vocabulary learning while browsing websites.

The extension will allow users to:

- Select words, expressions, or entire sentences on websites.
- Open a contextual translation window near the selected text.
- Translate selected text without leaving the current page.
- Select individual words or expressions inside the translation window.
- Create vocabulary cards directly from selected expressions.
- Preserve the original sentence as context for AI-generated cards.

The goal is to make vocabulary collection a natural part of everyday reading rather than a separate manual process.

### Learning Features

- Persistent learning statistics.
- Daily activity and streak tracking.
- Vocabulary progress synchronization.
- Improved word revision and learning tools.


## Technologies

### Current Frontend

- **React** — User interface development.
- **TypeScript** — Static typing and application logic.
- **Vite** — Development environment and build tooling.
- **CSS Modules** — Component-based styling.
- **Motion** — Interface transitions and layout animations.

### Planned Backend

- **C# / ASP.NET Core** — Backend API development.
- **Entity Framework Core** — Database access.
- **PostgreSQL** — Data persistence.
- **AI API Integration** — Vocabulary card generation and language processing.
- **Chrome Extension Manifest V3** — Browser integration.


## Project Status

🚧 **The project is currently under active development.**

The current development stage focuses on the frontend interface, interactive flashcards, vocabulary filtering, profile components, and UI animations.

Backend services, persistent data storage, AI integration, and the browser extension are planned for future development.

The application currently uses mock vocabulary data, and some interface elements are design prototypes awaiting backend integration.


## Motivation

Vocabulary started as a personal project to make English vocabulary learning more convenient without depending on paid flashcard applications.

Instead of studying only predefined word collections, the idea is to learn vocabulary naturally by collecting words and expressions encountered while reading articles, documentation, and other online content.

The application is initially being developed for personal use, with the possibility of becoming a publicly available learning platform in the future.
