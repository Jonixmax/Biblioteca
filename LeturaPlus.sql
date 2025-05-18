-- Crear la base de datos
CREATE DATABASE BibliotecaLectura21;
GO

USE BibliotecaLectura21;
GO

-- Crear tabla de libros
CREATE TABLE Libros (
    idLibro INT IDENTITY(1,1) PRIMARY KEY,
    titulo NVARCHAR(255) NOT NULL,
    autor NVARCHAR(255) NOT NULL,
    genero NVARCHAR(100) NOT NULL,
    sinopsis NVARCHAR(MAX) NOT NULL,
    portada_url NVARCHAR(500) NOT NULL
);
GO

-- Crear tabla de calificaciones
CREATE TABLE Calificaciones (
    idCalificacion INT IDENTITY(1,1) PRIMARY KEY,
    idLibro INT NOT NULL,
    puntuacion INT CHECK (puntuacion BETWEEN 1 AND 5),
    fechaHora DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (idLibro) REFERENCES Libros(idLibro) ON DELETE CASCADE
);
GO




-- Insertar libros de Ciencia Ficción
INSERT INTO Libros (titulo, autor, genero, sinopsis, portada_url) VALUES
('Dune', 'Frank Herbert', 'Ciencia Ficción', 'Una historia épica sobre política, religión y poder en un futuro lejano.', 'https://m.media-amazon.com/images/I/71-1WBgjGoL._SX342_.jpg'),
('Neuromante', 'William Gibson', 'Ciencia Ficción', 'El origen del ciberpunk, con inteligencia artificial y hackers.', 'https://m.media-amazon.com/images/I/51vkqHfvAyL._SY445_SX342_QL70_FMwebp_.jpg'),
('Fundación', 'Isaac Asimov', 'Ciencia Ficción', 'La caída y resurgimiento de un imperio galáctico.', 'https://m.media-amazon.com/images/I/91w19RSTuEL._SX342_.jpg');

-- Insertar libros de Fantasía
INSERT INTO Libros (titulo, autor, genero, sinopsis, portada_url) VALUES
('El Señor de los Anillos', 'J.R.R. Tolkien', 'Fantasía', 'Una épica aventura en la Tierra Media para destruir el Anillo Único.', 'https://m.media-amazon.com/images/I/514ahM7WRVL._SY445_SX342_QL70_FMwebp_.jpg'),
('Harry Potter y la Piedra Filosofal', 'J.K. Rowling', 'Fantasía', 'El comienzo de la historia del joven mago Harry Potter.', 'https://m.media-amazon.com/images/I/91pJ-MVbQ2L._SX342_.jpg'),
('Juego de Tronos', 'George R.R. Martin', 'Fantasía', 'La lucha por el trono de hierro en los Siete Reinos.', 'https://m.media-amazon.com/images/I/81Oeu1zPAlL._SX342_.jpg');

-- Insertar libros de Terror
INSERT INTO Libros (titulo, autor, genero, sinopsis, portada_url) VALUES
('It', 'Stephen King', 'Terror', 'La historia de un grupo de niños que enfrenta a un ente maligno.', 'https://m.media-amazon.com/images/I/714mHSkDVlL._SX342_.jpg'),
('Drácula', 'Bram Stoker', 'Terror', 'El clásico de los vampiros que define el género.', 'https://m.media-amazon.com/images/I/81iTqQPxj7L._SX342_.jpg'),
('El Exorcista', 'William Peter Blatty', 'Terror', 'El terror de una posesión demoníaca.', 'https://m.media-amazon.com/images/I/41BT5PFhbcL._SY445_SX342_QL70_FMwebp_.jpg');

-- Insertar libros de Romance
INSERT INTO Libros (titulo, autor, genero, sinopsis, portada_url) VALUES
('Orgullo y Prejuicio', 'Jane Austen', 'Romance', 'Un romance complicado en la Inglaterra del siglo XIX.', 'https://m.media-amazon.com/images/I/41-Wuk6VfCL._SY445_SX342_QL70_FMwebp_.jpg'),
('Bajo la Misma Estrella', 'John Green', 'Romance', 'Una historia de amor entre dos adolescentes con cáncer.', 'https://m.media-amazon.com/images/I/71LrW1jkrXL._SX342_.jpg'),
('Posdata: Te Amo', 'Cecelia Ahern', 'Romance', 'Una historia sobre el amor, la pérdida y la esperanza.', 'https://m.media-amazon.com/images/I/41brmktdLcL._SY445_SX342_QL70_FMwebp_.jpg');

-- Insertar libros de Misterio
INSERT INTO Libros (titulo, autor, genero, sinopsis, portada_url) VALUES
('El Código Da Vinci', 'Dan Brown', 'Misterio', 'Un thriller sobre secretos ocultos en la historia de la humanidad.', 'https://m.media-amazon.com/images/I/41-Oo4jkj1L._SY445_SX342_QL70_FMwebp_.jpg'),
('Sherlock Holmes: ', 'Arthur Conan Doyle', 'Misterio', 'El caso más famoso del detective Sherlock Holmes.', 'https://m.media-amazon.com/images/I/51ef9I3oz7L._SY445_SX342_QL70_FMwebp_.jpg'),
('La Chica del Tren', 'Paula Hawkins', 'Misterio', 'Un thriller psicológico con giros inesperados.', 'https://m.media-amazon.com/images/I/51sKhh+VEML._SY300_SX300_.jpg');

GO

select * from Libros