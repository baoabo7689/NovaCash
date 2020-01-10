DELIMITER // 

DROP PROCEDURE IF EXISTS UpdateBetDetailLastVersion;

CREATE PROCEDURE UpdateBetDetailLastVersion(
	IN Version TEXT
)

BEGIN    
	UPDATE Settings SET val=Version
	WHERE id="BetDetailLastVersion";
END //
 
 DELIMITER ;