export class Result<TData, TError>  {
  public Data: TData | null = null;
  public Error: TError | null = null;
  public IsError = this.Error !== null;

  private constructor(data: TData | null, error: TError | null){
    this.Data = data;
    this.Error = error;
  }

  public static success<TData,TError >(data: TData){
    return new Result<TData, TError>(data, null);
  }

  public static fail<TData,TError>(err: TError){
    return new Result<TData, TError>(null, err);
  }

};
