import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Member } from 'src/app/core/models/member.model';
import { ApiService } from 'src/app/core/services/api/api.service';

@Injectable()
export class MemberService {

    constructor(private readonly apiService: ApiService) {
    }

    public get(id: number): Observable<Member> {
        return this.apiService.get<Member>(`members/${id}`);
    }
}