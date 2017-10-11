VAR hasMirror = false

-> the_cell
=== the_cell ===
~isFree = false
You are in a prison cell, and you want to escape. There are some dirty sheets on the bed{ not hasMirror:, a mirror on the wall} and the door is locked from the outside.
    + [View Sheets] -> view_sheet
    + [View Mirror] -> view_mirror
    + [View Lock] -> view_lock
    
    = has_mirror
      You are still in your Cell, and you STILL want to escape! There are some dirty sheets on the bed, a mark where the mirror was, and that pesky door is still there, and firmly locked!
            ** [View Sheets] ->view_sheet.has_mirror
            ** [View lock] -> view_lock
            ** [Return roaming in your cell] -> the_cell
    ->DONE
->DONE

=== view_sheet ===
You can't believe you sleep in these things. Surely it's time somebody changed them. The pleasures of prison life I guess!
    + [Return roaming in your cell] -> the_cell

    = has_mirror
        Holding a mirror in your hand doesn't make the sheets look any better
            * [Return roaming in your cell] -> the_cell
    ->DONE
->END

=== view_mirror ===
{hasMirror: This is a dirty old mirror | The dirty old mirror on the wall seems loose.}
    * [Take the mirror]
    ~hasMirror = true
        -> the_cell.has_mirror
    + [Return to your cell] ->the_cell
->DONE

=== view_lock ===
This is one of those button locks. You have no idea what the combination is. You wish you could somehow see where the dirty fingerprints were, maybe that would help.
    + [Return to roaming your cell] ->the_cell
    + { hasMirror } [Use mirror] 
            You carefully put the mirror through the bars, and turn it round so you can see the lock. You can just make out fingerprints around the buttons. You press the dirty buttons, and hear a click.
            ++ [Open] ->the_corridor
            ++ [Return to your cell] -> the_cell
->END


