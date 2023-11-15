using _24CV_WEB.Models;
using _24CV_WEB.Repository;
using _24CV_WEB.Repository.CurriculumDAO;
using _24CV_WEB.Services.Contracts;
using System.Reflection.Metadata;

namespace _24CV_WEB.Services.ContractServices
{
    public class CurriculumService : ICurriculumService
    {
        private CurriculumRepository _repository;

        public CurriculumService(ApplicationDbContext context)
        {
            _repository = new CurriculumRepository(context);
        }

        public async Task<ResponseHelper> Create(Curriculum model)
        {
            ResponseHelper responseHelper = new ResponseHelper();

            try
            {
                string filePath = "";
                string fileName = "";

                if (model.Foto != null && model.Foto.Length > 0)
                {
                    fileName = Path.GetFileName(model.Foto.FileName);
                    filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Fotos", fileName);
                }

                model.RutaFoto = fileName;

                //Copia el archivo en un directorio
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await model.Foto.CopyToAsync(fileStream);
                }

                if (_repository.Create(model) > 0)
                {
                    responseHelper.Success = true;
                    responseHelper.Message = $"Se agregó el curriculum de {model.Nombre} con éxito.";
                }
                else
                {
                    responseHelper.Message = "Ocurrió un error al agregar el dato.";
                }
            }
            catch (Exception e)
            {
                responseHelper.Message = $"Ocurrió un error al agregar el dato. Error: {e.Message}";
            }


            return responseHelper;
        }

        public ResponseHelper Delete(int id)
        {
            ResponseHelper responseHelper = new ResponseHelper();

            try
            {
                Curriculum curriculum = _repository.GetById(id);

                if (curriculum != null)
                {
                    if (_repository.Delete(id) > 0)
                    {
                        responseHelper.Success = true;
                        responseHelper.Message = $"Se eliminó el currículum de {curriculum.Nombre} con éxito.";
                    }
                    else
                    {
                        responseHelper.Message = "Ocurrió un error al eliminar el currículum.";
                    }
                }
                else
                {
                    responseHelper.Message = "El currículum no se encontró en la base de datos.";
                }
            }
            catch (Exception e)
            {
                responseHelper.Message = $"Ocurrió un error al eliminar el currículum. Error: {e.Message}";
            }

            return responseHelper;
        }

        public List<Curriculum> GetAll()
        {
            List<Curriculum> list = new List<Curriculum>();

            try
            {
                list = _repository.GetAll();
            }
            catch (Exception e)
            {

                throw;
            }

            return list;
        }

        public Curriculum GetById(int id)
        {
            return _repository.GetById(id);
        }

        public ResponseHelper Update(Curriculum model)
        {
            ResponseHelper responseHelper = new ResponseHelper();

            try
            {
                if (_repository.Update(model) > 0)
                {
                    responseHelper.Success = true;
                    responseHelper.Message = $"Se actualizó el currículum de {model.Nombre} con éxito.";
                }
                else
                {
                    responseHelper.Message = "Ocurrió un error al actualizar el currículum.";
                }
            }
            catch (Exception e)
            {
                responseHelper.Message = $"Ocurrió un error al actualizar el currículum. Error: {e.Message}";
            }

            return responseHelper;
        }
    }
}