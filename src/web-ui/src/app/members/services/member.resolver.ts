import { Injectable } from '@angular/core';
import {
    Resolve,
    RouterStateSnapshot,
    ActivatedRouteSnapshot
} from '@angular/router';
import { Observable } from 'rxjs';
import { Member } from 'src/app/core/models/member.model';
import { MemberService } from './member.service';

@Injectable()
export class MemberResolver implements Resolve<Member> {

    constructor(private readonly memberService: MemberService) {
    }

    resolve(route: ActivatedRouteSnapshot, _: RouterStateSnapshot): Observable<Member> {
        const userId = route.paramMap.get("id");

        return this.memberService.get(+userId!);
    }
}
