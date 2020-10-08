CREATE TABLE users (
    id SERIAL PRIMARY KEY,
    username VARCHAR(25) UNIQUE NOT NULL,
    password VARCHAR(255) NOT NULL
);

CREATE TABLE genres (
    name VARCHAR(50) PRIMARY KEY
);

CREATE TABLE quizzes (
    id SERIAL PRIMARY KEY,
    owner_id INT REFERENCES users(id) NOT NULL,
    title VARCHAR(50) NOT NULL,
    genre VARCHAR(50) REFERENCES genres(name) NOT NULL,
    play_time INT NOT NULL,
    answer_time INT NOT NULL
);

CREATE TABLE songs (
    id INT PRIMARY KEY NOT NULL,
    spotify_id VARCHAR(255) UNIQUE NOT NULL
);

CREATE TABLE quiz_songs (
    id INT PRIMARY KEY,
    quiz_id INT REFERENCES quizzes(id) NOT NULL,
    song_id INT REFERENCES songs(id) NOT NULL
);

CREATE TABLE ratings (
    user_id INT PRIMARY KEY NOT NULL,
    rating REAL NOT NULL
);

CREATE TABLE scores (
    id SERIAL PRIMARY KEY,
    owner_id INT REFERENCES users(id) NOT NULL,
    quiz_id INT REFERENCES quizzes(id) NOT NULL,
    score INT NOT NULL
);