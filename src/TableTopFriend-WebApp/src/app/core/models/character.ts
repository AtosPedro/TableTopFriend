import { CharacterType } from "../constants/characterType";
import { CharacterSheet } from "./characterSheet";

export type Character = {
  userId: string,
  name: string,
  description: string,
  image: string,
  type: CharacterType,
  characterSheet: CharacterSheet
  audioEffectIds: string[]
}
