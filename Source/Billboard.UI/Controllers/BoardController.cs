using System;
using System.IO;
using System.Web.Mvc;
using Billboard.UI.Models.Board;
using Chucksoft.Core.Cryptography;
using Newtonsoft.Json;

namespace Billboard.UI.Controllers
{
    public class BoardController : Controller
    {
        //
        // GET: /Board/

        /// <summary>
        /// Indexes the specified id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>ActionResult.</returns>
        public ActionResult Index(int id)
        {
            return View(new BoardView(){EventId = id});
        }

        /// <summary>
        /// Messageses the specified id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>ActionResult.</returns>
        public ActionResult Messages(int id)
        {
            var messages = new[]
                               {
                                   new {Id=Randomizer.NextNumber(1000), From="916.123.1076", Body="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Maecenas erat nunc, varius quis fermentum sed, convallis eget mi. Sed tellus tellus, malesuada sed sollicitudin et, interdum ut sapien. ", Received=DateTime.Now.ToShortTimeString()},
                                   new {Id=Randomizer.NextNumber(1000), From="916.123.9076", Body="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Maecenas erat nunc, varius quis fermentum sed, convallis eget mi. Sed tellus tellus, malesuada sed sollicitudin et, interdum ut sapien. ", Received=DateTime.Now.ToShortTimeString()},
                                   new {Id=Randomizer.NextNumber(1000), From="916.123.0076", Body="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Maecenas erat nunc, varius quis fermentum sed, convallis eget mi. Sed tellus tellus, malesuada sed sollicitudin et, interdum ut sapien. ", Received=DateTime.Now.ToShortTimeString()},
                                   new {Id=Randomizer.NextNumber(1000), From="916.123.9076", Body="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Maecenas erat nunc, varius quis fermentum sed, convallis eget mi. Sed tellus tellus, malesuada sed sollicitudin et, interdum ut sapien. ", Received=DateTime.Now.ToShortTimeString()},
                                   new {Id=Randomizer.NextNumber(1000), From="916.123.9076", Body="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Maecenas erat nunc, varius quis fermentum sed, convallis eget mi. Sed tellus tellus, malesuada sed sollicitudin et, interdum ut sapien. ", Received=DateTime.Now.ToShortTimeString()},
                                   new {Id=Randomizer.NextNumber(1000), From="916.123.8076", Body="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Maecenas erat nunc, varius quis fermentum sed, convallis eget mi. Sed tellus tellus, malesuada sed sollicitudin et, interdum ut sapien. ", Received=DateTime.Now.ToShortTimeString()},
                                   new {Id=Randomizer.NextNumber(1000), From="916.123.9076", Body="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Maecenas erat nunc, varius quis fermentum sed, convallis eget mi. Sed tellus tellus, malesuada sed sollicitudin et, interdum ut sapien. ", Received=DateTime.Now.ToShortTimeString()},
                                   new {Id=Randomizer.NextNumber(1000), From="916.123.9076", Body="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Maecenas erat nunc, varius quis fermentum sed, convallis eget mi. Sed tellus tellus, malesuada sed sollicitudin et, interdum ut sapien. ", Received=DateTime.Now.ToShortTimeString()},
                                   new {Id=Randomizer.NextNumber(1000), From="916.123.9075", Body="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Maecenas erat nunc, varius quis fermentum sed, convallis eget mi. Sed tellus tellus, malesuada sed sollicitudin et, interdum ut sapien. ", Received=DateTime.Now.ToShortTimeString()},
                                   new {Id=Randomizer.NextNumber(1000), From="916.123.9076", Body="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Maecenas erat nunc, varius quis fermentum sed, convallis eget mi. Sed tellus tellus, malesuada sed sollicitudin et, interdum ut sapien. ", Received=DateTime.Now.ToShortTimeString()},
                                   new {Id=Randomizer.NextNumber(1000), From="916.123.6076", Body="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Maecenas erat nunc, varius quis fermentum sed, convallis eget mi. Sed tellus tellus, malesuada sed sollicitudin et, interdum ut sapien. ", Received=DateTime.Now.ToShortTimeString()},
                                   new {Id=Randomizer.NextNumber(1000), From="916.123.9074", Body="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Maecenas erat nunc, varius quis fermentum sed, convallis eget mi. Sed tellus tellus, malesuada sed sollicitudin et, interdum ut sapien. ", Received=DateTime.Now.ToShortTimeString()},
                                   new {Id=Randomizer.NextNumber(1000), From="916.123.9076", Body="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Maecenas erat nunc, varius quis fermentum sed, convallis eget mi. Sed tellus tellus, malesuada sed sollicitudin et, interdum ut sapien. ", Received=DateTime.Now.ToShortTimeString()},
                                   new {Id=Randomizer.NextNumber(1000), From="916.123.9073", Body="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Maecenas erat nunc, varius quis fermentum sed, convallis eget mi. Sed tellus tellus, malesuada sed sollicitudin et, interdum ut sapien. ", Received=DateTime.Now.ToShortTimeString()},
                                   new {Id=Randomizer.NextNumber(1000), From="916.123.9076", Body="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Maecenas erat nunc, varius quis fermentum sed, convallis eget mi. Sed tellus tellus, malesuada sed sollicitudin et, interdum ut sapien. ", Received=DateTime.Now.ToShortTimeString()},
                               };

            string json = JsonConvert.SerializeObject(messages);
            return Content(json, "application/json; charset=utf-8");
        }

    }
}
