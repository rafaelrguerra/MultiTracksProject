CREATE OR ALTER PROCEDURE [dbo].[GetArtistDetails]
	@artistID INT
AS
BEGIN

	SELECT TOP 1 title AS Artist, biography AS Biography, imageURL AS ArtistImage, heroURL AS ArtistHero FROM Artist WHERE ArtistID = @artistID
	
	SELECT album.title AS Album, album.imageURL AS AlbumImage, artist.title AS Artist FROM Album album
	JOIN Artist artist ON artist.artistID = Album.artistID
	WHERE album.ArtistID = @artistID

	SELECT a.imageURL AS AlbumImage, s.title AS Song, s.bpm AS BPM, a.title AS Album FROM Song s
	JOIN Album a ON a.albumID = s.albumID
	WHERE s.ArtistID = @artistID
END
