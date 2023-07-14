using DataAccess;
using System;
using System.Data;
using System.Linq;
using System.Web.UI;

public partial class artistDetails : MultitracksPage
{
    private const int artistTable = 0;
    private const int albumsTable = 1;
    private const int songsTable = 2;
    private readonly string artistNotFoundError = "Artist not found";
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.MaintainScrollPositionOnPostBack = true;

        if (Session["FullBiography"] == null || Session["AllAlbums"] == null || Session["AllSongs"] == null || !IsPostBack)
            FillArtistData();
    }
    private void FillArtistData()
    {
        try
        {
            var sql = new SQL();

            int artistId;
            int.TryParse(Request.QueryString["artistID"], out artistId);

            sql.Parameters.Add("artistID", artistId);
            var data = sql.ExecuteStoredProcedureDS("GetArtistDetails");

            //Artist table
            int artistIndex = data.Tables[artistTable].Columns["Artist"].Ordinal;
            int biographyIndex = data.Tables[artistTable].Columns["Biography"].Ordinal;
            int artistImageIndex = data.Tables[artistTable].Columns["ArtistImage"].Ordinal;
            int artistHeroIndex = data.Tables[artistTable].Columns["ArtistHero"].Ordinal;

            var artistValue = data.Tables[artistTable].Rows[0].ItemArray[artistIndex].ToString();
            Session["FullBiography"] = data.Tables[artistTable].Rows[0].ItemArray[biographyIndex].ToString();
            var artistImageValue = data.Tables[artistTable].Rows[0].ItemArray[artistImageIndex].ToString();
            var artistHeroValue = data.Tables[artistTable].Rows[0].ItemArray[artistHeroIndex].ToString();

            artist.Text = string.IsNullOrEmpty(artistValue) ? artistNotFoundError : artistValue;

            if (Session["FullBiography"].ToString().Length < 300)
                readMoreButton.Visible = false;

            biography.Text = Session["FullBiography"].ToString().Substring(0, 300) + "...";
            hero.ImageUrl = artistHeroValue;
            artistImage.ImageUrl = artistImageValue;

            //Album table
            Session["AllAlbums"] = data.Tables[albumsTable];

            if (((DataTable)Session["AllAlbums"]).AsEnumerable().Count() < 8)
                viewAllAlbumsButton.Visible = false;

            albumList.DataSource = ((DataTable)Session["AllAlbums"]).AsEnumerable().Take(8).CopyToDataTable();
            albumList.DataBind();

            //Song table
            Session["AllSongs"] = data.Tables[songsTable];

            if (((DataTable)Session["AllSongs"]).AsEnumerable().Count() < 3)
                viewAllSongsButton.Visible = false;

            songList.DataSource = ((DataTable)Session["AllSongs"]).AsEnumerable().Take(3).CopyToDataTable();
            songList.DataBind();

            viewAllSongsButton.Visible = true;
            viewAllAlbumsButton.Visible = true;
            readMoreButton.Visible = true;
        }
        catch
        {
            CleanPageAndSessions();
        }
    }

    private void CleanPageAndSessions()
    {
        artist.Text = artistNotFoundError;
        Session["AllSongs"] = null;
        Session["AllAlbums"] = null;
        Session["FullBiography"] = null;
        songsSection.Visible = false;
        albumsSection.Visible = false;
        biographySection.Visible = false;
        filtersHeader.Visible = false;
    }
    protected void viewAllSongs_Click(object sender, EventArgs e)
    {
        topSongs.InnerText = "All Songs";
        songList.DataSource = (DataTable)Session["AllSongs"];
        songList.DataBind();
        viewAllSongsButton.Visible = false;
    }
    protected void viewAllAlbums_Click(object sender, EventArgs e)
    {
        albumList.DataSource = (DataTable)Session["AllAlbums"];
        albumList.DataBind();
        viewAllAlbumsButton.Visible = false;
    }
    protected void readMore_Click(object sender, EventArgs e)
    {
        biography.Text = Session["FullBiography"].ToString();
        readMoreButton.Visible = false;
    }
    protected void overviewFilter_Click(object sender, EventArgs e)
    {
        songsSection.Visible = true;
        albumsSection.Visible = true;
        biographySection.Visible = true;

        overviewFilterLi.Attributes.Add("class", "discovery--nav--list--item tab-filter--item is-active");
        songsFilterLi.Attributes.Add("class", "discovery--nav--list--item tab-filter--item");
        albumsFilterLi.Attributes.Add("class", "discovery--nav--list--item tab-filter--item");
    }
    protected void songsFilter_Click(object sender, EventArgs e)
    {
        songsSection.Visible = true;
        albumsSection.Visible = false;
        biographySection.Visible = false;

        overviewFilterLi.Attributes.Add("class", "discovery--nav--list--item tab-filter--item");
        songsFilterLi.Attributes.Add("class", "discovery--nav--list--item tab-filter--item is-active");
        albumsFilterLi.Attributes.Add("class", "discovery--nav--list--item tab-filter--item");
    }

    protected void albumsFilter_Click(object sender, EventArgs e)
    {
        songsSection.Visible = false;
        albumsSection.Visible = true;
        biographySection.Visible = false;

        overviewFilterLi.Attributes.Add("class", "discovery--nav--list--item tab-filter--item");
        songsFilterLi.Attributes.Add("class", "discovery--nav--list--item tab-filter--item");
        albumsFilterLi.Attributes.Add("class", "discovery--nav--list--item tab-filter--item is-active");
    }
}