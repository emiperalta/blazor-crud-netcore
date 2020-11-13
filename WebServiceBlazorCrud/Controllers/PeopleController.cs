using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebServiceBlazorCrud.Models.Response;
using WebServiceBlazorCrud.Models;
using WebServiceBlazorCrud.Models.Request;

namespace WebServiceBlazorCrud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            Response res = new Response();

            try
            {
                using (PeopleBlazorCrudContext db = new PeopleBlazorCrudContext())
                {
                    var lst = db.People.ToList();
                    res.Success = 1;
                    res.Data = lst;
                }
            }
            catch (Exception ex)
            {
                res.Message = ex.Message;
            }

            return Ok(res);
        }

        [HttpGet("{id}")]
        public IActionResult GetOne(int? id)
        {
            Response res = new Response();

            try
            {
                using (PeopleBlazorCrudContext db = new PeopleBlazorCrudContext())
                {
                    Person p = db.People.Find(id);

                    if (p == null)
                    {
                        res.Success = 0;
                        res.Message = "Not found";
                        return NotFound(res);
                    }

                    res.Success = 1;
                }
            }
            catch (Exception ex)
            {
                res.Message = ex.Message;
            }

            return Ok(res);
        }

        [HttpPost]
        public IActionResult Add(PeopleRequest model)
        {
            Response res = new Response();

            try
            {
                using (PeopleBlazorCrudContext db = new PeopleBlazorCrudContext())
                {
                    Person p = new Person();
                    p.FirstName = model.FirstName;
                    p.LastName = model.LastName;

                    db.People.Add(p);
                    db.SaveChanges();
                    res.Success = 1;
                }
            }
            catch (Exception ex)
            {
                res.Message = ex.Message;
            }

            return Ok(res);
        }

        [HttpPut]
        public IActionResult Edit(PeopleRequest model)
        {
            Response res = new Response();

            try
            {
                using (PeopleBlazorCrudContext db = new PeopleBlazorCrudContext())
                {
                    Person p = db.People.Find(model.Id);

                    if (p == null)
                    {
                        res.Success = 0;
                        res.Message = "Not found";
                        return NotFound(res);
                    }

                    p.FirstName = model.FirstName;
                    p.LastName = model.LastName;

                    db.Entry(p).State = Microsoft.EntityFrameworkCore.EntityState.Modified; //to tell EF that entity has been modified
                    db.SaveChanges();
                    res.Success = 1;
                    res.Message = "Successfully updated";
                }
            }
            catch (Exception ex)
            {
                res.Message = ex.Message;
            }

            return Ok(res);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Response res = new Response();

            try
            {
                using (PeopleBlazorCrudContext db = new PeopleBlazorCrudContext())
                {
                    Person p = db.People.Find(id);

                    if (p == null)
                    {
                        res.Success = 0;
                        res.Message = "Not found";
                        return NotFound(res);
                    }

                    db.Remove(p);
                    db.SaveChanges();
                    res.Success = 1;
                    res.Message = "Successfully deleted";
                }
            }
            catch (Exception ex)
            {
                res.Message = ex.Message;
            }

            return Ok(res);
        }
    }
}
