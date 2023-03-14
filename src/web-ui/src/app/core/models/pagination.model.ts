export interface PagedResponse<T> {
    totalCount: number,
    currentPage: number,
    pageSize: number,
    items: T[];
}