using MemesAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MemesAPI.Controllers
{
    public class MemesController : ApiController
    {
        DBManager db = null;
        public MemesController()
        {
            if(db == null)
            {
                db = new DBManager();
            }
        }

        [AllowAnonymous]
        [Route("api/getallimages")]
        [HttpGet]
        public HttpResponseMessage getAllImages()
        {
            List<Memes> ds = db.GetAllImages();
            return Request.CreateResponse(HttpStatusCode.OK, new
            {
                success = true,
                message = "Success",
                data = ds
            });
            //return JsonConvert.SerializeObject(ds);
        }

        [Authorize(Roles = "admin")]
        [Route("api/addnewimage")]
        [HttpPost]
        public HttpResponseMessage AddNewImage(Memes values)
        {
            string conf = db.AddImage(values);
            return Request.CreateResponse(HttpStatusCode.OK, new
            {
                success = true,
                message = conf
            });
        }

        [AllowAnonymous]
        [Route("api/getimageswithtags")]
        [HttpPost]
        public HttpResponseMessage getimageswithtags(Memes values)
        {
            List<Memes> set = db.GetImagesWithTags(values.Tags);
            return Request.CreateResponse(HttpStatusCode.OK, new
            {
                success = true,
                message = "Success",
                data = set
            });
        }

        //shaklen's code below 

        [Authorize(Roles = "admin")]
        [Route("api/update")]
        [HttpPost]
        public HttpResponseMessage UpdateImage(Memes img)
        {

            string s=db.update_record(img);
            return Request.CreateResponse(HttpStatusCode.OK, new
            {
                success = true,
                message = "Success"
            });
        }


        [Authorize(Roles = "admin")]
        [Route("api/delete")]
        [HttpPost]
        public HttpResponseMessage Delete_Image(Memes img)
        {
            db.delete_record(img.ImageId);
            return Request.CreateResponse(HttpStatusCode.OK, new
            {
                success = true,
                message = "Success"
            });
        }


        [Authorize(Roles = "admin")]
        [Route("api/update_add_tag")]
        [HttpPost]
        public HttpResponseMessage update_add_tag(Memes img)
        {
            db.update_add_tag(img.ImageId, img.Tags);
            return Request.CreateResponse(HttpStatusCode.OK, new
            {
                success = true,
                message = "Success"
            });
        }


        [Authorize(Roles = "admin")]
        [Route("api/update_delete_tags")]
        [HttpPost]
        public HttpResponseMessage update_delete_tags(Memes img)
        {
            db.update_delete_tags(img.ImageId, img.Tags);
            return Request.CreateResponse(HttpStatusCode.OK, new
            {
                success = true,
                message = "Success"
            });
        }

        [Authorize(Roles = "admin")]
        [Route("api/getaccess")]
        [HttpGet]
        public HttpResponseMessage getAccess()
        {

            return Request.CreateResponse(HttpStatusCode.OK, new
            {
                success = true,
                message = "Success"
            });
        }



    }
}
