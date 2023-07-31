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
CREATE DATABASE seats; USE seats; <br>
CREATE TABLE seats ( <br>
  id INT NOT NULL AUTO_INCREMENT, <br>
  reserved_by VARCHAR(150) NOT NULL, <br>
  seat_number VARCHAR(150) NOT NULL, <br>
  status VARCHAR(150) NOT NULL, <br>
  reservation_time DATETIME NOT NULL, <br>
  purchase_done BOOLEAN NOT NULL DEFAULT(0), <br>
  PRIMARY KEY(id) <br>
  ); <br>
  INSERT INTO seats (reserved_by, seat_number, status, reservation_time, purchase_done) VALUES <br>
    ('none', 'seat-1', 'free', CURRENT_TIMESTAMP, 0); <br>
    ('none', 'seat-2', 'free', CURRENT_TIMESTAMP, 0); <br>
  
