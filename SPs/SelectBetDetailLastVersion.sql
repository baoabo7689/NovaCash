DELIMITER // 

DROP PROCEDURE IF EXISTS SelectBetDetailLastVersion;

CREATE PROCEDURE SelectBetDetailLastVersion()

BEGIN    
	SELECT val FROM Settings WHERE id="BetDetailLastVersion";
END //
 
 DELIMITER ;