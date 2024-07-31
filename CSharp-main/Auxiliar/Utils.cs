using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auxiliar
{
    public class Utils{}
    
    // PARA SALVAR NO BANCO SQL O DATETIME NOW SEM PRECISAR USAR O GETDATE()
    // 

    //   SinceDate = (String.IsNullOrEmpty(request.SinceDate.ToString())) ? null : Convert.ToDateTime(request.SinceDate).ToString("yyyy-MM-dd 00:00:00"),
    //   UntilDate = (String.IsNullOrEmpty(request.UntilDate.ToString())) ? null : Convert.ToDateTime(request.UntilDate).ToString("yyyy-MM-dd 23:59:59"),
    // (String.IsNullOrEmpty(request.Description) ? null : "%" + request.Description?.Trim() + "%"),



                  DateTime CreateAt = Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd   HH:mm:ss"),
                  DateTime  UpdateAt = Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd   HH:mm:ss"),

    //ListResponse<EstablishmentProject> project = _establishmentProjectRepository.GetProjects().Result;
    //voucherCardKitDesignViewModel.TagOptions = new SelectList(project.Items, "ProjectId", "Name");
    //EXEMPLO DE DIV PARA RECEBER UMA LISTA DE PARAMETROS, PRECISAR DO TIPO SELECTLIST
    //     @*      <div class="form-group has-feedback col-md-3">
    //                 <label asp-for="ProjectId">Projeto</label>
    //                 <select asp-items="Model.TagOptions" asp-for="ProjectId" id="ProjectId" class="form-control select2 select2-hidden-accessible" style="width: 100%;" data-select2-id="2" tabindex="-1" aria-hidden="true">
    //                 </select>
    //             </div>*@
    // 
      private DateTime ConvertToDate(string strDateTime)
        {
            strDateTime = $"{strDateTime.Substring(0, 4)}-{strDateTime.Substring(4, 2)}-{strDateTime.Substring(6, 2)}";
            return Convert.ToDateTime(strDateTime);
        }
        private TimeSpan ConvertToTime(string strTime)
        {
            strTime = $"{strTime.Substring(0, 2)}:{strTime.Substring(0,2)}:{strTime.Substring(0,2)}";
            return Convert.ToDateTime(strTime).TimeOfDay;
        }
        private DateTime ConvertToDateTime(string strDateTime)
        {
            DateTime dtFinaldate; string sDateTime;
            try { dtFinaldate = Convert.ToDateTime(strDateTime); }
            catch (Exception e)
            {
                string[] sDate = strDateTime.Split('/');
                sDateTime = sDate[1] + '/' + sDate[0] + '/' + sDate[2];
                dtFinaldate = Convert.ToDateTime(sDateTime);
            }
            return dtFinaldate;
        }

}