interface ModelError
{
    key: string;
    messages: string[];
}

export interface ResponseErrorObject
{
    type: string;
    title: string;
    status: number;
    errors: ModelError[];
}