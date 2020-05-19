
using AttendanceControl.API;
using AttendanceControl.API.Application.Contracts.DTOs;
using AttendanceControl.API.Business.Models;
using FluentAssertions;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    public class TeacherControllerTest
    {
        private readonly TestServer _testServer;
        private readonly HttpClient _httpClient;

        public TeacherControllerTest()
        {
            _testServer = new TestServer(WebHost.CreateDefaultBuilder()
                .UseStartup<Startup>());
            _httpClient = _testServer.CreateClient();
        }

        /// <summary>
        ///     Comprueba que una peticion de listado de profesores sin
        ///     autenticación retorna un 403
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task Get_teachers_without_auth_should_return_Unauthorized()
        {
            //Arrange
            var response = await _httpClient.GetAsync("api/teachers");

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);

        }

        /// <summary>
        ///     Comprueba que una peticion para crear un nuevo profesor
        ///     cuyo dni no tiene un formato de dni retorna un 400
        /// </summary>
        [Test]
        public async Task Wrong_dni_format_should_return_bad_request()
        {
            //Arrange
            Teacher teacher = new Teacher()
            {
                Dni = "htjrkdd",
                FirstName = "Jose",
                LastName1 = " García",
                LastName2 = " lopez"
            };

            var content = new StringContent(
                JsonConvert.SerializeObject(teacher)
                ,Encoding.UTF8, 
                "application/json");

            //Recupera el token de sesion antes
            string token = await this.SignIn();
    
            _httpClient.DefaultRequestHeaders
                .Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.PostAsync("api/teachers", content);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        }

        /// <summary>
        ///     Comprueba que una peticion para crear un nuevo profesor
        ///     cuyo datos personales han sido validados retorna un 200
        /// </summary>
        [Test]
        public async Task Save_teacher_should_return_ok()
        {
            //Arrange
            Teacher teacher = new Teacher()
            {
                Dni = "54257894Z",
                FirstName = "Jose",
                LastName1 = " García",
                LastName2 = " lopez"
            };

            var content = new StringContent(
                JsonConvert.SerializeObject(teacher)
                , Encoding.UTF8,
                "application/json");

            //Recupera el token de sesion antes
            string token = await this.SignIn();

            _httpClient.DefaultRequestHeaders
                .Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.PostAsync("api/teachers", content);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

        }

        /// <summary>
        ///     Comprueba que una peticion para crear un nuevo profesor
        ///     cuyo dni ya existe retorna un 409
        /// </summary>
        [Test]
        public async Task Dni_duplicate_entry_should_return_conflict()
        {
            //Arrange
            Teacher teacher = new Teacher()
            {
                Dni = "12345678R",
                FirstName = "Jose",
                LastName1 = " García",
                LastName2 = " lopez"
            };

            var content = new StringContent(
                JsonConvert.SerializeObject(teacher)
                , Encoding.UTF8,
                "application/json");

            //Recupera el token de sesion antes
            string token = await this.SignIn();

            _httpClient.DefaultRequestHeaders
                .Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.PostAsync("api/teachers", content);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.Conflict);

        }


        private async Task<string> SignIn()
        {
            Admin admin = new Admin()
            {
                AdminName = "admin",
                Password = "admin"
            };
            
            var content = new StringContent(
                JsonConvert.SerializeObject(admin)
                , Encoding.UTF8,
                "application/json");
            var response = await _httpClient.PostAsync("api/admins", content);

            string json = await response.Content.ReadAsStringAsync();
            AdminSignInResponse signInResponse =  JsonConvert.DeserializeObject<AdminSignInResponse>(json);

            return signInResponse.Token;
        }

       


    }
}