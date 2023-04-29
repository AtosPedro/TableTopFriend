import { Time } from "@angular/common";

export interface Session {
  id: string,
  userId: string,
  campaignId: string,
  name: string,
  dateTimeUtc: Date,
  duration: Time,
  charactersIds: string[],
  audioEffectIds: string[]
}
