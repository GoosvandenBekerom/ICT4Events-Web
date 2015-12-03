using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using SharedModels.Data.ContextInterfaces;
using SharedModels.Data.OracleContexts;
using SharedModels.Enums;
using SharedModels.FTP;
using SharedModels.Models;

namespace SharedModels.Logic
{
    public class MediaLogic
    {
        private readonly IMediaContext _context;

        public MediaLogic()
        {
            _context = new MediaOracleContext();
        }

        public MediaLogic(IMediaContext context)
        {
            _context = context;
        }

        public Media Insert(Media media)
        {
            return _context.Insert(media);
        }

        public bool Delete(Media media)
        {
            return _context.Delete(media);
        }

        public Media GetById(int id)
        {
            return _context.GetById(id);
        }

        public List<Media> GetAllById(int id, Event ev)
        {
            return _context.GetAllById(id, ev);
        }

        public List<Media> GetAllMedia(Event ev)
        {
            return _context.GetAllMedia(ev);
        }

        /// <summary>
        /// Checks the extension of the file and returns the mediatype
        /// </summary>
        /// <param name="ftpFileExtension">Extension to check</param>
        /// <returns>The appropriate mediatype belonging to the specified file extension</returns>
        public MediaType CheckExtension(string ftpFileExtension)
        {
            string[] imageEx = { ".jpg", ".jpeg", ".jpe", ".jfif", ".png" };
            string[] audioEx = { ".wav", ".mp3" };
            string[] videoEx = { ".mp4" };

            var type = MediaType.Image;
            if (imageEx.Contains(ftpFileExtension))
            {
                type = MediaType.Image;
            }
            else if (audioEx.Contains(ftpFileExtension))
            {
                type = MediaType.Audio;
            }
            else if (videoEx.Contains(ftpFileExtension))
            {
                type = MediaType.Video;
            }

            return type;
        }

        public Media UploadMedia(string localFilePath, User user, Event ev)
        {
            // Set file variables
            var ftpFileName = Path.GetFileName(localFilePath);
            var ftpFileExtension = Path.GetExtension(localFilePath);
            var ftpFileNameWithoutExtension = Path.GetFileNameWithoutExtension(localFilePath);

                Media media = null;
                if (!File.Exists(FtpHelper.ServerAddress + "/" + ev.ID))
                {
                    // Creates user & event map if not exsist on FTP server
                    FtpHelper.CreateDirectory(ev.ID.ToString());
                    if (!File.Exists(FtpHelper.ServerAddress + "/" + ev.ID + "/" + user.ID))
                    {
                        FtpHelper.CreateDirectory(ev.ID + "/" + user.ID);
                    }
                }

                if (FtpHelper.UploadFile(localFilePath, ev.ID + "/" + user.ID + "/" + ftpFileName))
                {
                    // Check file extension
                    var type = LogicCollection.MediaLogic.CheckExtension(ftpFileExtension);

                    // Makes the object media
                    media = new Media(0, user.ID, ev.ID, type, ftpFileNameWithoutExtension,
                        ftpFileExtension, ftpFileName);
                    media = LogicCollection.MediaLogic.Insert(media);
                }

                return media;
        }

        public bool DeleteMedia(Media media)
        {
            if (LogicCollection.MediaLogic.Delete(media))
            { 
                return true;
            }
            return false;
        }
    }
}
