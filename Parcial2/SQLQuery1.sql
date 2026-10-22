CREATE TABLE usuarios_USSS107526 (
    idUsuario INT IDENTITY(1,1) PRIMARY KEY,
    usuario VARCHAR(35) NOT NULL,
    clave VARCHAR(35) NOT NULL,
    nombre VARCHAR(65) NOT NULL,
    direccion VARCHAR(85) NOT NULL,
    telefono VARCHAR(9) NOT NULL
);

-- Insertar 3 registros iniciales
INSERT INTO usuarios_USSS107526 (usuario, clave, nombre, direccion, telefono)
VALUES 
('admin123', 'Pass1234', 'Juan Pérez', 'Usulután Centro', '7777-8888'),
('mlopez20', 'Clave2026', 'María López', 'San Salvador', '7123-4567'),
('carlos_m', 'SafePass1', 'Carlos Martínez', 'Usulután Este', '7890-1234');