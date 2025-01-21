"use client"

import * as React from "react"
import { Check, ChevronsUpDown } from "lucide-react"

import { cn } from "@/lib/utils"
import { Button } from "@/components/ui/button"
import {
    Command,
    CommandEmpty,
    CommandGroup,
    CommandInput,
    CommandItem,
    CommandList,
} from "@/components/ui/command"
import {
    Popover,
    PopoverContent,
    PopoverTrigger,
} from "@/components/ui/popover"


interface ComboBoxProps<TItem> {
    selectedValue?: string;
    defaultCaption: string;
    items: TItem[];
    getLabel: (item: TItem) => string;
    getKey: (item: TItem) => string;
    onSelect: (item: TItem) => void;
}

const Combobox = <T,>({ items, selectedValue, defaultCaption, getKey, getLabel, onSelect }: ComboBoxProps<T>) => {
    const [open, setOpen] = React.useState(false)
    const [value, setValue] = React.useState(selectedValue)

    return (
        <Popover open={open} onOpenChange={setOpen}>
            <PopoverTrigger asChild>
                <Button
                    variant="outline"
                    role="combobox"
                    aria-expanded={open}
                    className="w-[200px] justify-between"
                >
                    {value || defaultCaption}
                    <ChevronsUpDown className="ml-2 h-4 w-4 shrink-0 opacity-50" />
                </Button>
            </PopoverTrigger>
            <PopoverContent className="w-[200px] p-0">
                <Command>
                    <CommandInput placeholder="Search ..." />
                    <CommandList>
                        <CommandEmpty>Nothing found.</CommandEmpty>
                        <CommandGroup>
                            {items.map((item) => (
                                <CommandItem
                                    key={getKey(item)}
                                    value={getLabel(item)}
                                    onSelect={(currentValue) => {
                                        onSelect(item)
                                        setValue(currentValue === value ? "" : currentValue)
                                        setOpen(false)
                                    }}
                                >
                                    <Check
                                        className={cn(
                                            "mr-2 h-4 w-4",
                                            value === getLabel(item) ? "opacity-100" : "opacity-0"
                                        )}
                                    />
                                    {getLabel(item)}
                                </CommandItem>
                            ))}
                        </CommandGroup>
                    </CommandList>
                </Command>
            </PopoverContent>
        </Popover>
    )
}


export default Combobox;