DELIMITER // 

DROP PROCEDURE IF EXISTS SelectBetDetailLastVersion;

CREATE PROCEDURE SelectBetDetailLastVersion()

BEGIN    
	SELECT val FROM Settings WHERE id="BetDetailLastVersion" Limit 1;
END //
 
 DELIMITER ;