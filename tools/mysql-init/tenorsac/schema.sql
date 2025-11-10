CREATE TABLE Roles (
     id INT PRIMARY KEY AUTO_INCREMENT,
     value VARCHAR(20) NOT NULL
);
CREATE TABLE Users (
    id CHAR(36) PRIMARY KEY DEFAULT (UUID()),
    name VARCHAR(20) NOT NULL,
    email VARCHAR (255) UNIQUE NOT NULL,
    imageUrl VARCHAR(500) NULL,
    imageName VARCHAR(100) NULL,
    password VARCHAR(500) NOT NULL,
    roleId INT NOT NULL,
    active BOOL DEFAULT TRUE NOT NULL,
    creationDate DATETIME DEFAULT CURRENT_TIMESTAMP NOT NULL,
    FOREIGN KEY (roleId) REFERENCES Roles(id)
);
insert into Roles (value) values("admin");
insert into Roles (value) values("user");