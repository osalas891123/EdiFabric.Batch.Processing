using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using EdiFabric.Batch.Processing.Contracts.Application.Contracts.Interfaces;
using EdiFabric.Batch.Processing.Contracts.Application.Enums;
using EdiFabric.Templates.X12005010;

namespace EdiFabric.Batch.Processing.Services.Services
{
    public class Supervisor : ISupervisor
    {
        private readonly IFileProcessor _fileProcessor;

        public Supervisor(IFileProcessor fileProcessor)
        {
            _fileProcessor = fileProcessor;
        }

        public async Task<ReturnCode> SuperviseWork()
        {
            var result = ReturnCode.Failure;

            try
            {
                // Get files to process.
                var filesToProcess = _fileProcessor.GetEdiFilesToProcess();

                if (filesToProcess.Any())
                {
                    var resultCollection = new ConcurrentBag<IEnumerable<TS837>>();

                    var transactions = Parallel.ForEach(filesToProcess, (file) =>
                    {
                        resultCollection.Add(_fileProcessor.GetAllTransactionsFromFile(file));
                    });


                    // Do whatever you want with resultCollection. 

                    // Return success.
                    result = ReturnCode.TotalSuccess;
                }
                else 
                {
                    // Add logging.
                }
            }
            catch (SqlException ex)
            {
                // Add logging.
                // Return SqlException code.
                result = ReturnCode.SqlException;

            }
            catch (Exception ex)
            {
                // Add logging.
                // Return UnexpectedException code.
                result = ReturnCode.UnexpectedException;
            }

            return result;
        }
    }
}
