namespace TraineeManagementApi.Constants;

public  class RabbitMQConstants
{
    public const string QUEUE_NAME= "submissionprocessing";
    public const string DEAD_QUEUE_NAME= "submissionprocessing_dlq";



    public const string X_DEAD_LETTER_EXCHANGE = "submissionprocessing_dlx";

    public const string X_DEAD_LETTER_EXCHANGE_KEY = "submissionprocessing_dlx_key";
}