 export interface IPagingPageQueryDto<TFilters>
 {
   filters: Partial<TFilters> | null;
   pageSize: number;
   totalRecordCount: number;
   pageIndex: number;
}
