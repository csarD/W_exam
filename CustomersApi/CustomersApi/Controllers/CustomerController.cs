
using CustomersApi.Dtos;
using CustomersApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace CustomersApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class CustomerController : Controller
    {

        private readonly CustomerDatabaseContext _customerDatabaseContext;
        public CustomerController(CustomerDatabaseContext customerDatabaseContext)
        {
            _customerDatabaseContext = customerDatabaseContext;
            
        }


        //api/customer/{id}
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CreateDataDto))]

        public async Task<IActionResult> CreateCustomer(CreateDataDto customer)
        {
            int[] height = new int[] { 0, 1, 0, 2, 1, 0, 1, 3, 2, 1, 2, 1 };
            int max = height.Max();
            int columns = height.Length;
            int[,] x = new int[columns, max];


            //definiendo alturas
            for (int i = 0; i < columns; i++)
            {
                int height_node = height[i];

                for (int j = 0; j < height_node; j++)
                {
                    x[i, j] = 1;
                }
            }

            //console.log(x)

            for (int j = 0; j < max; j++)
            {

                int[] huecos = new int[columns];
                int[] elevaciones = new int[columns];
                for (int i = 1; i < columns; i++)
                {

                    while (x[i, j] == 1)
                    {
                        elevaciones[elevaciones.Length-1] = i;
                        if (i < (columns - 1))
                        {
                            i++;
                        }
                        else
                        {
                            break;
                        }
                    }
                    huecos[huecos.Length-1] = i;
                }
                //llenando de agua
                if (elevaciones.Length > 1)
                {

                    for (int k = 1; k < elevaciones.Length; k++)
                    {

                        for (int m = elevaciones[k - 1] + 1; m < elevaciones[k]; m++)
                        {

                            x[m, j] = 2;


                        }

                    }

                }

            }
            //cuenta de los huecos de agua
            int count = 0;
            for (int i = 0; i < columns; i++)
            {


                for (int j = 0; j < max; j++)
                {
                    if (x[i,j] == 2)
                    {
                        count++;
                    }
                }
            }

           // DataEntity result = await _customerDatabaseContext.Add(customer);
            return new OkObjectResult(count);

        }
    }
}
