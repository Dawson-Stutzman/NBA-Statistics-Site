IF OBJECT_ID(N'PlayerGame') IS NULL
BEGIN
	CREATE TABLE NBA.PlayerGame
	(
		PlayerGameID INT NOT NULL IDENTITY(1,1),
		PlayerSeasonID INT NOT NULL FOREIGN KEY REFERENCES NBA.PlayerSeason(PlayerSeasonID),
		GameID INT NOT NULL FOREIGN KEY REFERENCES NBA.Game(GameID),
		[Minutes] DECIMAL(4,2) NOT NULL,
		FieldGoalAttempts INT NOT NULL,
		FieldGoalMade INT NOT NULL,
		ThreePointAttempts INT NOT NULL,
		ThreePointMade INT NOT NULL,
		FreeThrowAttempts INT NOT NULL,
		FreeThrowMade INT NOT NULL,
		Rebounds INT NOT NULL,
		Assists INT NOT NULL,
		Blocks INT NOT NULL,
		Steals INT NOT NULL

		UNIQUE(GameID, PlayerSeasonID)
	);
END