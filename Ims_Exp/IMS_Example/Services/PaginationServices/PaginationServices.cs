using IMS_Example.Response;

namespace IMS_Example.Services.PaginationServices
{
    public interface IPaginationServices<T>
    {
        Task<PaginationResponse<ICollection<T>>> paginationListTableAsync(ICollection<T> taskList, int? pageIndex, int pageSize);

    }
    public class PaginationServices<T> : IPaginationServices<T>
    {
        public Task<PaginationResponse<ICollection<T>>> paginationListTableAsync(ICollection<T> taskList, int? pageIndex, int pageSize)
        {
            var sucess = true;
            var message = "Get all data";
            var data = taskList;
            var toPage = 0.0;
            var totalPage = 0;

            if (pageSize > 0)
            {
                toPage = Math.Ceiling(taskList.ToList().Count / (float)pageSize);
                totalPage = (int)toPage;
            }

            if (!pageIndex.HasValue)
            {
                var result = new PaginationResponse<ICollection<T>>(sucess, message, data, totalPage);

                return Task.FromResult(result);
            }

            if (taskList.Any())
            {
                if ((double)pageIndex > totalPage || pageIndex <= 0)
                {
                    sucess = false;
                    message = "This page doesn't exist!";
                    data = null;
                    var result = new PaginationResponse<ICollection<T>>(sucess, message, data, totalPage);

                    return Task.FromResult(result);
                }

                message = $"Get all data in page {pageIndex}";
                data = taskList.Skip((pageIndex.Value - 1) * pageSize).Take(pageSize).ToList();
                var resultPage = new PaginationResponse<ICollection<T>>(sucess, message, data, totalPage);
                return Task.FromResult(resultPage);
            }

            return Task.FromResult(new PaginationResponse<ICollection<T>>(sucess, message, data, totalPage));
        }
    }



}
