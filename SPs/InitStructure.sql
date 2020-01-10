
CREATE TABLE IF NOT EXISTS Settings (
	id VARCHAR(255) PRIMARY KEY,
	val TEXT
) ENGINE=INNODB;

INSERT INTO Settings(id, val) VALUES("BetDetailLastVersion", 0); 


CREATE TABLE IF NOT EXISTS Transactions (
	trans_id BIGINT PRIMARY KEY,
	vendor_member_id VARCHAR(30) NOT NULL,
	stake DECIMAL(30, 4),
	winlost_amount DECIMAL(30, 4),
	val JSON
) ENGINE=INNODB;



