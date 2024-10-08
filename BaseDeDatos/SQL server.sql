CREATE DATABASE [programacion-asp-net];

USE [programacion-asp-net]

CREATE TABLE identificacion (
    identificacion_id INT PRIMARY KEY IDENTITY(1,1),
    email VARCHAR(255) NOT NULL,
    contra_hash VARBINARY(255) NOT NULL
);

CREATE TABLE usuario (
    usuario_id INT PRIMARY KEY IDENTITY(1,1),
    nombre VARCHAR(255) NOT NULL,
    fecha_alta DATETIME NOT NULL,
    activo BIT NOT NULL,
    identificacion_id INT,
    FOREIGN KEY (identificacion_id) REFERENCES identificacion(identificacion_id)
);

CREATE TABLE roles (
    rol_id INT PRIMARY KEY IDENTITY(1,1),
    nombre_rol VARCHAR(255) NOT NULL
);

CREATE TABLE usuario_rol (
    usuario_id INT PRIMARY KEY IDENTITY(1,1),
    rol_id INT,
    FOREIGN KEY (usuario_id) REFERENCES usuario(usuario_id),
    FOREIGN KEY (rol_id) REFERENCES roles(rol_id)
);

CREATE TABLE registro_actividades (
    actividad_id INT PRIMARY KEY IDENTITY(1,1),
    usuario_id INT,
    actividad VARCHAR(255) NOT NULL,
    descripcion VARCHAR(255),
    fecha DATETIME NOT NULL DEFAULT GETDATE(),
    FOREIGN KEY (usuario_id) REFERENCES usuario(usuario_id)
);

INSERT INTO roles values('Admin');

INSERT INTO identificacion (email, contra_hash)
VALUES ('admin@admin.com', CONVERT(VARBINARY(255), HASHBYTES('SHA2_256', 'admin1234')));

INSERT INTO usuario (nombre, fecha_alta, activo, identificacion_id)
VALUES (
    'Admin',
    GETDATE(),
    1,
    (SELECT SCOPE_IDENTITY() FROM identificacion WHERE email = 'admin@admin.com')
);

SELECT * FROM usuario JOIN identificacion ON usuario.identificacion_id = identificacion.identificacion_id;