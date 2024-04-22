IF DB_ID('NBA_Statistics') IS NULL
BEGIN
    CREATE DATABASE NBA_Statistics;
END

USE NBA

IF NOT EXISTS
   (
      SELECT *
      FROM sys.schemas s
      WHERE s.[name] = N'Statistics'
   )
BEGIN
   EXEC(N'CREATE SCHEMA [Statistics] AUTHORIZATION [dbo]');
END;



IF OBJECT_ID(N'[Statistics].Team') IS NULL
BEGIN
	CREATE TABLE [Statistics].Team
	(
		TeamID INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
		[Name] NVARCHAR(32) NOT NULL,
		ConferenceName NVARCHAR(32) NOT NULL,
		UNIQUE([Name])
	);

INSERT INTO [Statistics].Team ([Name], ConferenceName) VALUES ('Atlanta Hawks', 'Eastern');
INSERT INTO [Statistics].Team ([Name], ConferenceName) VALUES ('Boston Celtics', 'Eastern');
INSERT INTO [Statistics].Team ([Name], ConferenceName) VALUES ('Brooklyn Nets', 'Eastern');
INSERT INTO [Statistics].Team ([Name], ConferenceName) VALUES ('Charlotte Hornets', 'Eastern');
INSERT INTO [Statistics].Team ([Name], ConferenceName) VALUES ('Chicago Bulls', 'Eastern');
INSERT INTO [Statistics].Team ([Name], ConferenceName) VALUES ('Cleveland Cavaliers', 'Eastern');
INSERT INTO [Statistics].Team ([Name], ConferenceName) VALUES ('Dallas Mavericks', 'Western');
INSERT INTO [Statistics].Team ([Name], ConferenceName) VALUES ('Denver Nuggets', 'Western');
INSERT INTO [Statistics].Team ([Name], ConferenceName) VALUES ('Detroit Pistons', 'Eastern');
INSERT INTO [Statistics].Team ([Name], ConferenceName) VALUES ('Golden State Warriors', 'Western');
INSERT INTO [Statistics].Team ([Name], ConferenceName) VALUES ('Houston Rockets', 'Western');
INSERT INTO [Statistics].Team ([Name], ConferenceName) VALUES ('Indiana Pacers', 'Eastern');
INSERT INTO [Statistics].Team ([Name], ConferenceName) VALUES ('Los Angeles Clippers', 'Western');
INSERT INTO [Statistics].Team ([Name], ConferenceName) VALUES ('Los Angeles Lakers', 'Western');
INSERT INTO [Statistics].Team ([Name], ConferenceName) VALUES ('Memphis Grizzlies', 'Eastern');
INSERT INTO [Statistics].Team ([Name], ConferenceName) VALUES ('Miami Heat', 'Eastern');
INSERT INTO [Statistics].Team ([Name], ConferenceName) VALUES ('Milwaukee Bucks', 'Eastern');
INSERT INTO [Statistics].Team ([Name], ConferenceName) VALUES ('Minnesota Timberwolves', 'Western');
INSERT INTO [Statistics].Team ([Name], ConferenceName) VALUES ('New Orleans Pelicans', 'Western');
INSERT INTO [Statistics].Team ([Name], ConferenceName) VALUES ('New York Knicks', 'Eastern');
INSERT INTO [Statistics].Team ([Name], ConferenceName) VALUES ('Oklahoma City Thunder', 'Western');
INSERT INTO [Statistics].Team ([Name], ConferenceName) VALUES ('Orlando Magic', 'Eastern');
INSERT INTO [Statistics].Team ([Name], ConferenceName) VALUES ('Philadelphia 76ers', 'Eastern');
INSERT INTO [Statistics].Team ([Name], ConferenceName) VALUES ('Phoenix Suns', 'Western');
INSERT INTO [Statistics].Team ([Name], ConferenceName) VALUES ('Portland Trail Blazers', 'Western');
INSERT INTO [Statistics].Team ([Name], ConferenceName) VALUES ('Sacramento Kings', 'Western');
INSERT INTO [Statistics].Team ([Name], ConferenceName) VALUES ('San Antonio Spurs', 'Western');
INSERT INTO [Statistics].Team ([Name], ConferenceName) VALUES ('Toronto Raptors', 'Eastern');
INSERT INTO [Statistics].Team ([Name], ConferenceName) VALUES ('Utah Jazz', 'Western');
INSERT INTO [Statistics].Team ([Name], ConferenceName) VALUES ('Washington Wizards', 'Western');
END





IF OBJECT_ID(N'Statistics.TeamSeason') IS NULL
BEGIN 
	CREATE TABLE [Statistics].TeamSeason
	(
		TeamSeasonID INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
		TeamID INT NOT NULL FOREIGN KEY REFERENCES [Statistics].Team (TeamID),
		[Year] NVARCHAR(16) NOT NULL,
		Verified INT NOT NULL DEFAULT 0


		UNIQUE(TeamID, [Year])
	);

INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'Atlanta Hawks' = T.[Name]), N'2022-23', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'Atlanta Hawks' = T.[Name]), N'2021-22', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'Atlanta Hawks' = T.[Name]), N'2020-21', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'Atlanta Hawks' = T.[Name]), N'2016-17', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'Atlanta Hawks' = T.[Name]), N'2015-16', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'Atlanta Hawks' = T.[Name]), N'2014-15', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'Atlanta Hawks' = T.[Name]), N'2013-14', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'Boston Celtics' = T.[Name]), N'2022-23', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'Boston Celtics' = T.[Name]), N'2021-22', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'Boston Celtics' = T.[Name]), N'2020-21', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'Boston Celtics' = T.[Name]), N'2019-20', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'Boston Celtics' = T.[Name]), N'2018-19', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'Boston Celtics' = T.[Name]), N'2017-18', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'Boston Celtics' = T.[Name]), N'2016-17', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'Boston Celtics' = T.[Name]), N'2015-16', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'Boston Celtics' = T.[Name]), N'2014-15', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'Brooklyn Nets' = T.[Name]), N'2022-23', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'Brooklyn Nets' = T.[Name]), N'2021-22', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'Brooklyn Nets' = T.[Name]), N'2020-21', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'Brooklyn Nets' = T.[Name]), N'2019-20', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'Brooklyn Nets' = T.[Name]), N'2018-19', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'Brooklyn Nets' = T.[Name]), N'2014-15', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'Brooklyn Nets' = T.[Name]), N'2013-14', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'Charlotte Hornets' = T.[Name]), N'2015-16', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'Charlotte Hornets' = T.[Name]), N'2013-14', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'Chicago Bulls' = T.[Name]), N'2021-22', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'Chicago Bulls' = T.[Name]), N'2016-17', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'Chicago Bulls' = T.[Name]), N'2014-15', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'Chicago Bulls' = T.[Name]), N'2013-14', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'Cleveland Cavaliers' = T.[Name]), N'2022-23', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'Cleveland Cavaliers' = T.[Name]), N'2017-18', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'Cleveland Cavaliers' = T.[Name]), N'2016-17', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'Cleveland Cavaliers' = T.[Name]), N'2015-16', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'Cleveland Cavaliers' = T.[Name]), N'2014-15', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'Dallas Mavericks' = T.[Name]), N'2021-22', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'Dallas Mavericks' = T.[Name]), N'2020-21', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'Dallas Mavericks' = T.[Name]), N'2019-20', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'Dallas Mavericks' = T.[Name]), N'2015-16', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'Dallas Mavericks' = T.[Name]), N'2014-15', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'Dallas Mavericks' = T.[Name]), N'2013-14', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'Denver Nuggets' = T.[Name]), N'2022-23', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'Denver Nuggets' = T.[Name]), N'2021-22', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'Denver Nuggets' = T.[Name]), N'2020-21', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'Denver Nuggets' = T.[Name]), N'2019-20', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'Denver Nuggets' = T.[Name]), N'2018-19', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'Detroit Pistons' = T.[Name]), N'2018-19', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'Detroit Pistons' = T.[Name]), N'2015-16', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'Golden State Warriors' = T.[Name]), N'2022-23', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'Golden State Warriors' = T.[Name]), N'2021-22', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'Golden State Warriors' = T.[Name]), N'2018-19', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'Golden State Warriors' = T.[Name]), N'2017-18', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'Golden State Warriors' = T.[Name]), N'2016-17', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'Golden State Warriors' = T.[Name]), N'2015-16', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'Golden State Warriors' = T.[Name]), N'2014-15', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'Golden State Warriors' = T.[Name]), N'2013-14', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'Houston Rockets' = T.[Name]), N'2019-20', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'Houston Rockets' = T.[Name]), N'2018-19', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'Houston Rockets' = T.[Name]), N'2017-18', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'Houston Rockets' = T.[Name]), N'2016-17', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'Houston Rockets' = T.[Name]), N'2015-16', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'Houston Rockets' = T.[Name]), N'2014-15', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'Houston Rockets' = T.[Name]), N'2013-14', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'Indiana Pacers' = T.[Name]), N'2019-20', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'Indiana Pacers' = T.[Name]), N'2018-19', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'Indiana Pacers' = T.[Name]), N'2017-18', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'Indiana Pacers' = T.[Name]), N'2016-17', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'Indiana Pacers' = T.[Name]), N'2015-16', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'Indiana Pacers' = T.[Name]), N'2013-14', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'Los Angeles Clippers' = T.[Name]), N'2022-23', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'Los Angeles Clippers' = T.[Name]), N'2020-21', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'Los Angeles Clippers' = T.[Name]), N'2019-20', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'Los Angeles Clippers' = T.[Name]), N'2018-19', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'Los Angeles Clippers' = T.[Name]), N'2016-17', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'Los Angeles Clippers' = T.[Name]), N'2015-16', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'Los Angeles Clippers' = T.[Name]), N'2014-15', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'Los Angeles Clippers' = T.[Name]), N'2013-14', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'Los Angeles Lakers' = T.[Name]), N'2022-23', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'Los Angeles Lakers' = T.[Name]), N'2020-21', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'Los Angeles Lakers' = T.[Name]), N'2019-20', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'Memphis Grizzlies' = T.[Name]), N'2022-23', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'Memphis Grizzlies' = T.[Name]), N'2021-22', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'Memphis Grizzlies' = T.[Name]), N'2020-21', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'Memphis Grizzlies' = T.[Name]), N'2016-17', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'Memphis Grizzlies' = T.[Name]), N'2015-16', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'Memphis Grizzlies' = T.[Name]), N'2014-15', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'Memphis Grizzlies' = T.[Name]), N'2013-14', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'Miami Heat' = T.[Name]), N'2022-23', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'Miami Heat' = T.[Name]), N'2021-22', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'Miami Heat' = T.[Name]), N'2020-21', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'Miami Heat' = T.[Name]), N'2019-20', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'Miami Heat' = T.[Name]), N'2017-18', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'Miami Heat' = T.[Name]), N'2015-16', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'Miami Heat' = T.[Name]), N'2013-14', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'Milwaukee Bucks' = T.[Name]), N'2022-23', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'Milwaukee Bucks' = T.[Name]), N'2021-22', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'Milwaukee Bucks' = T.[Name]), N'2020-21', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'Milwaukee Bucks' = T.[Name]), N'2019-20', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'Milwaukee Bucks' = T.[Name]), N'2018-19', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'Milwaukee Bucks' = T.[Name]), N'2017-18', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'Milwaukee Bucks' = T.[Name]), N'2016-17', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'Milwaukee Bucks' = T.[Name]), N'2014-15', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'Minnesota Timberwolves' = T.[Name]), N'2022-23', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'Minnesota Timberwolves' = T.[Name]), N'2021-22', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'Minnesota Timberwolves' = T.[Name]), N'2017-18', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'New Orleans Pelicans' = T.[Name]), N'2021-22', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'New Orleans Pelicans' = T.[Name]), N'2017-18', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'New Orleans Pelicans' = T.[Name]), N'2014-15', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'New York Knicks' = T.[Name]), N'2022-23', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'New York Knicks' = T.[Name]), N'2020-21', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'Oklahoma City Thunder' = T.[Name]), N'2019-20', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'Oklahoma City Thunder' = T.[Name]), N'2018-19', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'Oklahoma City Thunder' = T.[Name]), N'2017-18', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'Oklahoma City Thunder' = T.[Name]), N'2016-17', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'Oklahoma City Thunder' = T.[Name]), N'2015-16', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'Oklahoma City Thunder' = T.[Name]), N'2014-15', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'Oklahoma City Thunder' = T.[Name]), N'2013-14', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'Orlando Magic' = T.[Name]), N'2019-20', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'Orlando Magic' = T.[Name]), N'2018-19', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'Philadelphia 76Ers' = T.[Name]), N'2022-23', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'Philadelphia 76Ers' = T.[Name]), N'2021-22', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'Philadelphia 76Ers' = T.[Name]), N'2020-21', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'Philadelphia 76Ers' = T.[Name]), N'2019-20', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'Philadelphia 76Ers' = T.[Name]), N'2018-19', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'Philadelphia 76Ers' = T.[Name]), N'2017-18', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'Phoenix Suns' = T.[Name]), N'2022-23', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'Phoenix Suns' = T.[Name]), N'2021-22', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'Phoenix Suns' = T.[Name]), N'2020-21', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'Portland Trail Blazers' = T.[Name]), N'2020-21', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'Portland Trail Blazers' = T.[Name]), N'2019-20', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'Portland Trail Blazers' = T.[Name]), N'2018-19', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'Portland Trail Blazers' = T.[Name]), N'2017-18', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'Portland Trail Blazers' = T.[Name]), N'2016-17', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'Portland Trail Blazers' = T.[Name]), N'2015-16', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'Portland Trail Blazers' = T.[Name]), N'2014-15', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'Portland Trail Blazers' = T.[Name]), N'2013-14', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'Sacramento Kings' = T.[Name]), N'2022-23', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'San Antonio Spurs' = T.[Name]), N'2018-19', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'San Antonio Spurs' = T.[Name]), N'2017-18', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'San Antonio Spurs' = T.[Name]), N'2016-17', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'San Antonio Spurs' = T.[Name]), N'2015-16', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'San Antonio Spurs' = T.[Name]), N'2014-15', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'San Antonio Spurs' = T.[Name]), N'2013-14', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'Toronto Raptors' = T.[Name]), N'2021-22', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'Toronto Raptors' = T.[Name]), N'2019-20', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'Toronto Raptors' = T.[Name]), N'2018-19', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'Toronto Raptors' = T.[Name]), N'2017-18', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'Toronto Raptors' = T.[Name]), N'2016-17', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'Toronto Raptors' = T.[Name]), N'2015-16', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'Toronto Raptors' = T.[Name]), N'2014-15', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'Toronto Raptors' = T.[Name]), N'2013-14', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'Utah Jazz' = T.[Name]), N'2021-22', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'Utah Jazz' = T.[Name]), N'2020-21', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'Utah Jazz' = T.[Name]), N'2019-20', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'Utah Jazz' = T.[Name]), N'2018-19', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'Utah Jazz' = T.[Name]), N'2017-18', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'Utah Jazz' = T.[Name]), N'2016-17', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'Washington Wizards' = T.[Name]), N'2020-21', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'Washington Wizards' = T.[Name]), N'2017-18', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'Washington Wizards' = T.[Name]), N'2016-17', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'Washington Wizards' = T.[Name]), N'2014-15', 1);
INSERT [Statistics].TeamSeason (TeamID, [Year], Verified) VALUES ((SELECT T.TeamID FROM [Statistics].Team T WHERE N'Washington Wizards' = T.[Name]), N'2013-14', 1);


END

SELECT *
FROM [Statistics].TeamSeason
/*
IF OBJECT_ID(N'PlayerSeason')IS NULL
BEGIN
	CREATE TABLE NBA.PlayerSeason
	(
		PlayerSeasonID INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
		PlayerID INT NOT NULL FOREIGN KEY REFERENCES NBA.Player(PlayerID),
		TeamSeasonID INT NOT NULL FOREIGN KEY REFERENCES NBA.TeamSeason(TeamSeasonID),

		UNIQUE(PlayerID, TeamSeasonID)
	);
END

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



IF OBJECT_ID(N'Game') IS NULL
BEGIN
   CREATE TABLE NBA.Game
   (
      GameID INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	  HomeTeamID INT NOT NULL,
	  AwayTeamID INT NOT NULL,
	  [Year] INT NOT NULL,
	  [Date] NVARCHAR(32) NOT NULL,

	  CONSTRAINT FK_HomeTeam_Year FOREIGN KEY (HomeTeamID, [Year]) REFERENCES NBA.TeamSeason(TeamID, [Year]),
	  CONSTRAINT FK_AwayTeam_Year FOREIGN KEY (HomeTeamID, [Year]) REFERENCES NBA.TeamSeason(TeamID, [Year])
   );
END
*/