using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedModels.Enums;
using SharedModels.Models;

namespace SharedModels.Data.ContextInterfaces
{
    public interface IMediaContext : IRepositoryContext<Media>
    {
        List<Media> GetAllById(int guestId, Event ev);
        List<Media> GetAllMedia(Event ev);
    }
}
