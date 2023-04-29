import { CharacterType } from "../constants/characterType";
import { CharacterSheet } from "./characterSheet";

export interface Character {
  userId: string,
  name: string,
  description: string,
  image: string,
  type: CharacterType,
  characterSheet: CharacterSheet
  audioEffectIds: string[]
}
