using vanillapdf.net.PdfSyntax;
using vanillapdf.net.PdfUtils;

namespace vanillapdf.net.Examples
{
    public class CustomFileWriterObserverContextExample : PdfFileWriterObserverContext
    {
        public override uint OnAfterEntryOffsetRecalculation(PdfXrefEntry data)
        {
            return PdfReturnValues.ERROR_SUCCESS;
        }

        public override uint OnAfterObjectOffsetRecalculation(PdfObject data)
        {
            return PdfReturnValues.ERROR_SUCCESS;
        }

        public override uint OnAfterObjectWrite(PdfObject data)
        {
            return PdfReturnValues.ERROR_SUCCESS;
        }

        public override uint OnAfterOutputFlush(PdfInputOutputStream data)
        {
            return PdfReturnValues.ERROR_SUCCESS;
        }

        public override uint OnBeforeEntryOffsetRecalculation(PdfXrefEntry data)
        {
            return PdfReturnValues.ERROR_SUCCESS;
        }

        public override uint OnBeforeObjectOffsetRecalculation(PdfObject data)
        {
            return PdfReturnValues.ERROR_SUCCESS;
        }

        public override uint OnBeforeObjectWrite(PdfObject data)
        {
            return PdfReturnValues.ERROR_SUCCESS;
        }

        public override uint OnBeforeOutputFlush(PdfInputOutputStream data)
        {
            return PdfReturnValues.ERROR_SUCCESS;
        }

        public override uint OnFinalizing(PdfInputOutputStream data)
        {
            return PdfReturnValues.ERROR_SUCCESS;
        }

        public override uint OnInitializing(PdfInputOutputStream data)
        {
            return PdfReturnValues.ERROR_SUCCESS;
        }
    }
}
