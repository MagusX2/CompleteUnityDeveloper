
VAR hasHairClip = false
VAR isDressedUp = false
VAR isTheEnd = false
VAR isFree = false

=== the_corridor ===
 You're out of your cell, but not out of trouble. You are in the corridor, there's a closet and some stairs leading to the courtyard. There's also various detritus on the floor
    + [View the closet] -> view_closet
    +[Inspect the floor] -> inspect_floor
    + [Climb the stairs] -> climb_stairs
    
    = has_clip
        Still in the corridor. Floor still dirty. Hairclip in hand. Now what? 
            ++ [Return to corridor] -> the_corridor
            ++ [Climb the stairs] ->climb_stairs.has_clip
    ->DONE

    = declined_dressing
      Back in the corridor, having declined to dress-up as a cleaner
                ++ [Revisit the closet] -> inside_closet
                ++ [Climb the stairs] -> climb_stairs.has_clip
    ->DONE
    
    = dress_up
     You're standing back in the corridor, now convincingly dressed as a cleaner. You strongly consider the run for freedom.
        * [Take the stairs] 
            You walk through the courtyard dressed as a cleaner. The guard tips his hat at you as you waltz past, claiming your freedom. You heart races as you walk into the sunset. --- The End ---
                ~isTheEnd = true
                ~isFree = true
        + [Undress]
            ~isDressedUp = false
            -> inside_closet
    ->DONE
->DONE

=== view_closet ===
    You are looking at a closet door, unfortunately it's locked. {hasHairClip:You wonder if that lock on the closet would succumb to some lock-picking?| Maybe you could find something around to help encourage it open.}
        + [Return to the corridor] -> the_corridor
        + {hasHairClip}  [Pick the lock] -> inside_closet
->DONE

=== inspect_floor ===
    {hasHairClip: You found an hairclip on the dirty floor earlier | Rummagaing around on the dirty floor, you find a hairclip.}
        + [Return to the corridor] -> the_corridor
        * [Take the hairclip] 
        ~hasHairClip = true
        -> the_corridor.has_clip
                    
->DONE

=== climb_stairs ===
{hasHairClip: -> has_clip | -> climb_stairs.no_clip}

    = has_clip
         Unfortunately weilding a puny hairclip hasn't given you the confidence to walk out into a courtyard surrounded by armed guards!
            + [Retreat down the stairs] -> the_corridor.has_clip
    ->DONE
    
    = no_clip
    You start walking up the stairs towards the outside light. You realise it's not break time, and you'll be caught immediately. You slither back down the stairs and reconsider
        + [Return to the corridor] -> the_corridor
    ->DONE

    = is_dressed
        You feel smug for picking the closet door open, and are still armed with a hairclip (now badly bent). Even these achievements together don't give you the courage to climb up the stairs to your death!
            + [Return to the corridor] -> the_corridor.has_clip
    ->DONE
->DONE

=== inside_closet ===
    Inside the closet you see a cleaner's uniform that looks about your size! Seems like your day is looking-up.
        + [Dress up] 
            ~ isDressedUp = true
            -> the_corridor.dress_up
        + [Return to the corridor] ->the_corridor.declined_dressing

->DONE