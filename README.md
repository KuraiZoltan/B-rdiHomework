# Bardi Homework
# Project
Movie Theater online ticket shop/seat reservation application made in ASP.Net MVC using a MySql Database.

# Packages
The project uses the packages:
  1. MySqlConnector
  2. MySql.Data
  3. MimeKit
  4. MailKit

# Database Setup
CREATE DATABASE seats; USE seats;
CREATE TABLE seats (
  id INT NOT NULL AUTO_INCREMENT,
  reserved_by VARCHAR(150) NOT NULL,
  seat_number VARCHAR(150) NOT NULL,
  status VARCHAR(150) NOT NULL,
  reservation_time DATETIME NOT NULL,
  purchase_done BOOLEAN NOT NULL DEFAULT(0),
  PRIMARY KEY(id)
  );
  INSERT INTO seats (reserved_by, seat_number, status, reservation_time, purchase_done) VALUES
    ('none', 'seat-1', 'free', CURRENT_TIMESTAMP, 0);
    ('none', 'seat-2', 'free', CURRENT_TIMESTAMP, 0);
  
