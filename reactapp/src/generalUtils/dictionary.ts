interface DictionaryItem<TKey, TValue>
{
    key: TKey;
    value: TValue;
}

/**
 * A simple dictionary.
 */
export interface Dictionary<TKey, TValue> extends Array<DictionaryItem<TKey, TValue>>
{
    
}

/**
 * A simple read only dictionary.
 */
export interface ReadOnlyDictionary<TKey, TValue> extends ReadonlyArray<DictionaryItem<TKey, TValue>>
{

}