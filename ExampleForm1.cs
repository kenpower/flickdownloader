using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using FlickrNet;
using System.Net;

namespace Coding4Fun_1
{
    public partial class ExampleForm1 : TemplateForm
    {
        public ExampleForm1()
        {
            InitializeComponent();
        }

        private void SimpleSearchButton1_Click(object sender, EventArgs e)
        {
            OutputTextbox.Text = "";

            // Example 1
            //string apikey = ApiKey.Text;
            string apikey ="deaba23a63d9b3c34b63f1a04548a3f4";
            Flickr flickr = new Flickr(apikey);

            // Example 2
            PhotoSearchOptions searchOptions = new PhotoSearchOptions();
            searchOptions.UserId = "70831453@N00";
            searchOptions.Tags = "kanas";
            searchOptions.PerPage = 500;
            //PhotoCollection microsoftPhotos = flickr.PhotosSearch(searchOptions);
            //PhotosetPhotoCollection myset = flickr.PhotosetsGetPhotos("72157594398087814");
            PhotoCollection myset = flickr.PhotosSearch(searchOptions);
            //Photoset myset = flickr.PhotosetsGetInfo("72157594398087814");
            

            // Example 3
            //searchOptions.Page = 2;
            //PhotoCollection microsoftPhotos2 = flickr.PhotosSearch(searchOptions);
            //searchOptions.Page = 3;
            //PhotoCollection microsoftPhotos3 = flickr.PhotosSearch(searchOptions);

            // Eample 4
            List<Photo> allPhotos = new List<Photo>();
            allPhotos.AddRange(myset);
            //allPhotos.AddRange(microsoftPhotos2);
            //allPhotos.AddRange(microsoftPhotos3);

            WebClient webClient = new WebClient();

            String s = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            foreach (Photo photo in allPhotos)
            {

                OutputTextbox.Text += "Photos title is " + photo.Title + " " + photo.ThumbnailUrl+ "\r\n";

                if (photo.DoesLargeExist){
                    webClient.DownloadFile(photo.LargeUrl, s + @"\temp\" + photo.PhotoId +searchOptions.Tags+ @".jpg");
                }
                else if(photo.DoesMediumExist){
                    webClient.DownloadFile(photo.MediumUrl, s+@"\temp\"+photo.PhotoId+searchOptions.Tags+@".jpg");

                }
                else
                {
                    webClient.DownloadFile(photo.SmallUrl , s + @"\temp\" + photo.PhotoId + searchOptions.Tags+@".jpg");

                }
              
                

            }
        }

        private void FindUserButton_Click(object sender, EventArgs e)
        {
            // First page of the users photos
            // Sorted by interestingness

            Flickr flickr = new Flickr(ApiKey.Text);
            FoundUser user;
            try
            {
                user = flickr.PeopleFindByUserName(Username.Text);
                OutputTextbox.Text = "User Id = " + user.UserId + "\r\n" + "Username = " + user.UserName + "\r\n";
            }
            catch (FlickrException ex)
            {
                OutputTextbox.Text = ex.Message;
                return;
            }

            PhotoSearchOptions userSearch = new PhotoSearchOptions();
            userSearch.UserId = user.UserId;
            userSearch.SortOrder = PhotoSearchSortOrder.InterestingnessDescending;
            PhotoCollection usersPhotos = flickr.PhotosSearch(userSearch);
            // Get users contacts
            ContactCollection contacts = flickr.ContactsGetPublicList(user.UserId);
            // Get first page of a users favorites
            PhotoCollection usersFavoritePhotos = flickr.FavoritesGetPublicList(user.UserId);
            // Get a list of the users groups
            //PublicGroupInfoCollection usersGroups = flickr.PeopleGetPublicGroups(user.UserId);

            int i = 0;
            foreach (Contact contact in contacts)
            {
                OutputTextbox.Text += "Contact " + contact.UserName + "\r\n";
                if (i++ > 10) break; // only list the first 10
            }

            i = 0;
            //foreach (PublicGroupInfo group in usersGroups)
            //{
            //    OutputTextbox.Text += "Group " + group.GroupName + "\r\n";
            //    if (i++ > 10) break; // only list the first 10
            //}

            i = 0;
            foreach (Photo photo in usersPhotos)
            {
                OutputTextbox.Text += "Interesting photo title is " + photo.Title + " " + photo.WebUrl + "\r\n";
                if (i++ > 10) break; // only list the first 10
            }

            i = 0;
            foreach (Photo photo in usersFavoritePhotos)
            {
                OutputTextbox.Text += "Favourite photo title is " + photo.Title + " " + photo.WebUrl + "\r\n";
                if (i++ > 10) break; // only list the first 10
            }
        }
    }
}


