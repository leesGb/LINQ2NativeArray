using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Collections;

namespace pcysl5edgo.Collections.LINQ
{
    public struct
        WhereEnumerable<TPrevEnumerable, TPrevEnumerator, TSource, TPredicate>
        : IRefEnumerable<WhereEnumerable<TPrevEnumerable, TPrevEnumerator, TSource, TPredicate>.Enumerator, TSource>, ILinq<TSource>
        where TSource : unmanaged
        where TPredicate : struct, IRefFunc<TSource, bool>
        where TPrevEnumerator : struct, IRefEnumerator<TSource>
        where TPrevEnumerable : struct, IRefEnumerable<TPrevEnumerator, TSource>
    {
        private TPrevEnumerable enumerable;
        private readonly TPredicate predicts;

        internal WhereEnumerable(in TPrevEnumerable enumerable, TPredicate predicts)
        {
            this.enumerable = enumerable;
            this.predicts = predicts;
        }

        public Enumerator GetEnumerator()
            => new Enumerator(enumerable.GetEnumerator(), predicts);

        IEnumerator<TSource> IEnumerable<TSource>.GetEnumerator() => GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        #region Enumerable
        public AppendEnumerable<WhereEnumerable<TPrevEnumerable, TPrevEnumerator, TSource, TPredicate>, Enumerator, TSource> Append(TSource value, Allocator allocator = Allocator.Temp)
            => new AppendEnumerable<WhereEnumerable<TPrevEnumerable, TPrevEnumerator, TSource, TPredicate>, Enumerator, TSource>(this, value, allocator);

        public WhereEnumerable<TPrevEnumerable, TPrevEnumerator, TSource, TPredicate> AsRefEnumerable() => this;

        public DefaultIfEmptyEnumerable<WhereEnumerable<TPrevEnumerable, TPrevEnumerator, TSource, TPredicate>, Enumerator, TSource>
            DefaultIfEmpty(TSource defaultValue, Allocator allocator = Allocator.Temp)
            => new DefaultIfEmptyEnumerable<WhereEnumerable<TPrevEnumerable, TPrevEnumerator, TSource, TPredicate>, Enumerator, TSource>(this, defaultValue, allocator);

        public DistinctEnumerable<
                WhereEnumerable<TPrevEnumerable, TPrevEnumerator, TSource, TPredicate>,
                Enumerator, TSource, DefaultEqualityComparer<TSource>, DefaultGetHashCodeFunc<TSource>>
            Distinct(Allocator allocator = Allocator.Temp)
            => new DistinctEnumerable<WhereEnumerable<TPrevEnumerable, TPrevEnumerator, TSource, TPredicate>, Enumerator, TSource, DefaultEqualityComparer<TSource>, DefaultGetHashCodeFunc<TSource>>(this, default, default, allocator);

        public DistinctEnumerable<
                WhereEnumerable<TPrevEnumerable, TPrevEnumerator, TSource, TPredicate>,
                Enumerator, TSource, TEqualityComparer, TGetHashCodeFunc>
            Distinct<TEqualityComparer, TGetHashCodeFunc>(TEqualityComparer comparer, TGetHashCodeFunc getHashCodeFunc, Allocator allocator = Allocator.Temp)
            where TEqualityComparer : struct, IRefFunc<TSource, TSource, bool>
            where TGetHashCodeFunc : struct, IRefFunc<TSource, int>
            => new DistinctEnumerable<WhereEnumerable<TPrevEnumerable, TPrevEnumerator, TSource, TPredicate>, Enumerator, TSource, TEqualityComparer, TGetHashCodeFunc>(this, comparer, getHashCodeFunc, allocator);

        public SelectEnumerable<WhereEnumerable<TPrevEnumerable, TPrevEnumerator, TSource, TPredicate>, Enumerator, TSource, TResult, TAction> Select<TResult, TAction>(TAction action, Allocator allocator = Allocator.Temp)
            where TResult : unmanaged
            where TAction : unmanaged, IRefAction<TSource, TResult>
            => new SelectEnumerable<WhereEnumerable<TPrevEnumerable, TPrevEnumerator, TSource, TPredicate>, Enumerator, TSource, TResult, TAction>(this, action, allocator);

        public SelectIndexEnumerable<WhereEnumerable<TPrevEnumerable, TPrevEnumerator, TSource, TPredicate>, Enumerator, TSource, TResult, TAction> SelectIndex<TResult, TAction>(TAction action, Allocator allocator = Allocator.Temp)
            where TResult : unmanaged
            where TAction : unmanaged, ISelectIndex<TSource, TResult>
            => new SelectIndexEnumerable<WhereEnumerable<TPrevEnumerable, TPrevEnumerator, TSource, TPredicate>, Enumerator, TSource, TResult, TAction>(this, action, allocator);

        public SelectManyEnumerable<
                WhereEnumerable<TPrevEnumerable, TPrevEnumerator, TSource, TPredicate>,
                Enumerator,
                TSource,
                TResult,
                TResultEnumerable,
                TResultEnumerator,
                TResultAction
            >
            SelectMany<TResult, TResultEnumerable, TResultEnumerator, TResultAction>(TResultAction action)
            where TResult : unmanaged
            where TResultEnumerator : struct, IRefEnumerator<TResult>
            where TResultEnumerable : struct, IRefEnumerable<TResultEnumerator, TResult>
            where TResultAction : struct, IRefAction<TSource, TResultEnumerable>
            => new SelectManyEnumerable<WhereEnumerable<TPrevEnumerable, TPrevEnumerator, TSource, TPredicate>, Enumerator, TSource, TResult, TResultEnumerable, TResultEnumerator, TResultAction>(this, action);

        public
            WhereIndexEnumerable<
                WhereEnumerable<TPrevEnumerable, TPrevEnumerator, TSource, TPredicate>,
                Enumerator,
                TSource,
                DefaultSkipIndex<TSource>
            >
            Skip(long count)
            => new WhereIndexEnumerable<WhereEnumerable<TPrevEnumerable, TPrevEnumerator, TSource, TPredicate>, Enumerator, TSource, DefaultSkipIndex<TSource>>(this, new DefaultSkipIndex<TSource>(count));

        public
            WhereIndexEnumerable<
                WhereEnumerable<TPrevEnumerable, TPrevEnumerator, TSource, TPredicate>,
                Enumerator,
                TSource,
                DefaultSkipWhileIndex<TSource, TPredicate0>
            >
            SkipWhileIndex<TPredicate0>(TPredicate0 predicate)
            where TPredicate0 : struct, IWhereIndex<TSource>
            => new WhereIndexEnumerable<WhereEnumerable<TPrevEnumerable, TPrevEnumerator, TSource, TPredicate>, Enumerator, TSource, DefaultSkipWhileIndex<TSource, TPredicate0>>(this, new DefaultSkipWhileIndex<TSource, TPredicate0>(predicate));

        public
            WhereIndexEnumerable<
                WhereEnumerable<TPrevEnumerable, TPrevEnumerator, TSource, TPredicate>,
                Enumerator,
                TSource,
                DefaultTakeIndex<TSource>
            >
            Take(long count)
            => new WhereIndexEnumerable<WhereEnumerable<TPrevEnumerable, TPrevEnumerator, TSource, TPredicate>, Enumerator, TSource, DefaultTakeIndex<TSource>>(this, new DefaultTakeIndex<TSource>(count));

        public
            WhereIndexEnumerable<
                WhereEnumerable<TPrevEnumerable, TPrevEnumerator, TSource, TPredicate>,
                Enumerator,
                TSource,
                DefaultTakeWhileIndex<TSource, TPredicate0>
            >
            TakeWhileIndex<TPredicate0>(TPredicate0 predicate)
            where TPredicate0 : struct, IWhereIndex<TSource>
            => new WhereIndexEnumerable<WhereEnumerable<TPrevEnumerable, TPrevEnumerator, TSource, TPredicate>, Enumerator, TSource, DefaultTakeWhileIndex<TSource, TPredicate0>>(this, new DefaultTakeWhileIndex<TSource, TPredicate0>(predicate));

        public WhereEnumerable<WhereEnumerable<TPrevEnumerable, TPrevEnumerator, TSource, TPredicate>, Enumerator, TSource, TPredicate0> Where<TPredicate0>(TPredicate0 predicate)
            where TPredicate0 : unmanaged, IRefFunc<TSource, bool>
            => new WhereEnumerable<WhereEnumerable<TPrevEnumerable, TPrevEnumerator, TSource, TPredicate>, Enumerator, TSource, TPredicate0>(this, predicate);

        public WhereIndexEnumerable<
                WhereEnumerable<TPrevEnumerable, TPrevEnumerator, TSource, TPredicate>,
                Enumerator, TSource, TPredicate0>
            WhereIndex<TPredicate0>(TPredicate0 predicate)
            where TPredicate0 : unmanaged, IWhereIndex<TSource>
            => new WhereIndexEnumerable<WhereEnumerable<TPrevEnumerable, TPrevEnumerator, TSource, TPredicate>, Enumerator, TSource, TPredicate0>(this, predicate);

        public ZipEnumerable<
                WhereEnumerable<TPrevEnumerable, TPrevEnumerator, TSource, TPredicate>
                , Enumerator, TSource, TEnumerable0, TEnumerator0, TSource0, TResult0, TAction0>
            Zip<TEnumerable0, TEnumerator0, TSource0, TResult0, TAction0>
            (in TEnumerable0 second, TAction0 action, TSource firstDefaultValue = default, TSource0 secondDefaultValue = default, Allocator allocator = Allocator.Temp)
            where TEnumerable0 : struct, IRefEnumerable<TEnumerator0, TSource0>
            where TEnumerator0 : struct, IRefEnumerator<TSource0>
            where TSource0 : unmanaged
            where TResult0 : unmanaged
            where TAction0 : unmanaged, IRefAction<TSource, TSource0, TResult0>
            => new ZipEnumerable<WhereEnumerable<TPrevEnumerable, TPrevEnumerator, TSource, TPredicate>, Enumerator, TSource, TEnumerable0, TEnumerator0, TSource0, TResult0, TAction0>(this, second, action, firstDefaultValue, secondDefaultValue, allocator);
        #endregion

        #region Concat
        public ConcatEnumerable<
                WhereEnumerable<TPrevEnumerable, TPrevEnumerator, TSource, TPredicate>,
                Enumerator,
                TEnumerable1,
                TEnumerator1,
                TSource
            >
            Concat<TEnumerable1, TEnumerator1>
            (in TEnumerable1 second)
            where TEnumerable1 : struct, IRefEnumerable<TEnumerator1, TSource>
            where TEnumerator1 : struct, IRefEnumerator<TSource>
            => new ConcatEnumerable<WhereEnumerable<TPrevEnumerable, TPrevEnumerator, TSource, TPredicate>, Enumerator, TEnumerable1, TEnumerator1, TSource>(this, second);

        public ConcatEnumerable<
                WhereEnumerable<TPrevEnumerable, TPrevEnumerator, TSource, TPredicate>,
                Enumerator,
                NativeEnumerable<TSource>,
                NativeEnumerable<TSource>.Enumerator,
                TSource
            >
            Concat(NativeArray<TSource> second)
            => new ConcatEnumerable<WhereEnumerable<TPrevEnumerable, TPrevEnumerator, TSource, TPredicate>, Enumerator, NativeEnumerable<TSource>, NativeEnumerable<TSource>.Enumerator, TSource>(this, second.AsRefEnumerable());

        public ConcatEnumerable<
                WhereEnumerable<TPrevEnumerable, TPrevEnumerator, TSource, TPredicate>,
                Enumerator,
                NativeEnumerable<TSource>,
                NativeEnumerable<TSource>.Enumerator,
                TSource
            >
            Concat(in NativeEnumerable<TSource> second)
            => new ConcatEnumerable<WhereEnumerable<TPrevEnumerable, TPrevEnumerator, TSource, TPredicate>, Enumerator, NativeEnumerable<TSource>, NativeEnumerable<TSource>.Enumerator, TSource>(this, second);

        public ConcatEnumerable<
                WhereEnumerable<TPrevEnumerable, TPrevEnumerator, TSource, TPredicate>,
                Enumerator,
                ConcatEnumerable<TEnumerable1, TEnumerator1, TEnumerable2, TEnumerator2, TSource>,
                ConcatEnumerable<TEnumerable1, TEnumerator1, TEnumerable2, TEnumerator2, TSource>.Enumerator,
                TSource
            >
            Concat<TEnumerable1, TEnumerator1, TEnumerable2, TEnumerator2>
            (in ConcatEnumerable<TEnumerable1, TEnumerator1, TEnumerable2, TEnumerator2, TSource> second)
            where TEnumerable1 : struct, IRefEnumerable<TEnumerator1, TSource>
            where TEnumerator1 : struct, IRefEnumerator<TSource>
            where TEnumerable2 : struct, IRefEnumerable<TEnumerator2, TSource>
            where TEnumerator2 : struct, IRefEnumerator<TSource>
            => new ConcatEnumerable<WhereEnumerable<TPrevEnumerable, TPrevEnumerator, TSource, TPredicate>, Enumerator, ConcatEnumerable<TEnumerable1, TEnumerator1, TEnumerable2, TEnumerator2, TSource>, ConcatEnumerable<TEnumerable1, TEnumerator1, TEnumerable2, TEnumerator2, TSource>.Enumerator, TSource>(this, second);

        public ConcatEnumerable<
                WhereEnumerable<TPrevEnumerable, TPrevEnumerator, TSource, TPredicate>,
                Enumerator,
                AppendEnumerable<TEnumerable1, TEnumerator1, TSource>,
                AppendEnumerable<TEnumerable1, TEnumerator1, TSource>.Enumerator,
                TSource
            >
            Concat<TEnumerable1, TEnumerator1>
            (in AppendEnumerable<TEnumerable1, TEnumerator1, TSource> second)
            where TEnumerable1 : struct, IRefEnumerable<TEnumerator1, TSource>
            where TEnumerator1 : struct, IRefEnumerator<TSource>
            => new ConcatEnumerable<WhereEnumerable<TPrevEnumerable, TPrevEnumerator, TSource, TPredicate>, Enumerator, AppendEnumerable<TEnumerable1, TEnumerator1, TSource>, AppendEnumerable<TEnumerable1, TEnumerator1, TSource>.Enumerator, TSource>(this, second);


        public ConcatEnumerable<
                WhereEnumerable<TPrevEnumerable, TPrevEnumerator, TSource, TPredicate>,
                Enumerator,
                ArrayEnumerable<TSource>,
                ArrayEnumerable<TSource>.Enumerator,
                TSource
            >
            Concat(in ArrayEnumerable<TSource> second)
            => new ConcatEnumerable<WhereEnumerable<TPrevEnumerable, TPrevEnumerator, TSource, TPredicate>, Enumerator, ArrayEnumerable<TSource>, ArrayEnumerable<TSource>.Enumerator, TSource>(this, second);

        public ConcatEnumerable<
                WhereEnumerable<TPrevEnumerable, TPrevEnumerator, TSource, TPredicate>,
                Enumerator,
                ArrayEnumerable<TSource>,
                ArrayEnumerable<TSource>.Enumerator,
                TSource
            >
            Concat(TSource[] second)
            => new ConcatEnumerable<WhereEnumerable<TPrevEnumerable, TPrevEnumerator, TSource, TPredicate>, Enumerator, ArrayEnumerable<TSource>, ArrayEnumerable<TSource>.Enumerator, TSource>(this, second.AsRefEnumerable());


        public ConcatEnumerable<
                WhereEnumerable<TPrevEnumerable, TPrevEnumerator, TSource, TPredicate>,
                Enumerator,
                DefaultIfEmptyEnumerable<TEnumerable1, TEnumerator1, TSource>,
                DefaultIfEmptyEnumerable<TEnumerable1, TEnumerator1, TSource>.Enumerator,
                TSource
            >
            Concat<TEnumerable1, TEnumerator1>
            (in DefaultIfEmptyEnumerable<TEnumerable1, TEnumerator1, TSource> second)
            where TEnumerable1 : struct, IRefEnumerable<TEnumerator1, TSource>
            where TEnumerator1 : struct, IRefEnumerator<TSource>
            => new ConcatEnumerable<
                    WhereEnumerable<TPrevEnumerable, TPrevEnumerator, TSource, TPredicate>,
                    Enumerator,
                    DefaultIfEmptyEnumerable<TEnumerable1, TEnumerator1, TSource>,
                    DefaultIfEmptyEnumerable<TEnumerable1, TEnumerator1, TSource>.Enumerator,
                    TSource>
                (this, second);

        public ConcatEnumerable<
                WhereEnumerable<TPrevEnumerable, TPrevEnumerator, TSource, TPredicate>,
                Enumerator,
                DistinctEnumerable<TEnumerable1, TEnumerator1, TSource, TEqualityComparer, TGetHashCodeFunc>,
                DistinctEnumerable<TEnumerable1, TEnumerator1, TSource, TEqualityComparer, TGetHashCodeFunc>.Enumerator,
                TSource
            >
            Concat<TEnumerable1, TEnumerator1, TEqualityComparer, TGetHashCodeFunc>
            (in DistinctEnumerable<TEnumerable1, TEnumerator1, TSource, TEqualityComparer, TGetHashCodeFunc> second)
            where TEnumerable1 : struct, IRefEnumerable<TEnumerator1, TSource>
            where TEnumerator1 : struct, IRefEnumerator<TSource>
            where TEqualityComparer : struct, IRefFunc<TSource, TSource, bool>
            where TGetHashCodeFunc : struct, IRefFunc<TSource, int>
            => new ConcatEnumerable<WhereEnumerable<TPrevEnumerable, TPrevEnumerator, TSource, TPredicate>, Enumerator, DistinctEnumerable<TEnumerable1, TEnumerator1, TSource, TEqualityComparer, TGetHashCodeFunc>, DistinctEnumerable<TEnumerable1, TEnumerator1, TSource, TEqualityComparer, TGetHashCodeFunc>.Enumerator, TSource>(this, second);

        public ConcatEnumerable<
                WhereEnumerable<TPrevEnumerable, TPrevEnumerator, TSource, TPredicate>,
                Enumerator,
                RangeRepeatEnumerable<TSource, TAction>,
                RangeRepeatEnumerable<TSource, TAction>.Enumerator,
                TSource
            >
            Concat<TAction>
            (in RangeRepeatEnumerable<TSource, TAction> second)
            where TAction : struct, IRefAction<TSource>
            => new ConcatEnumerable<WhereEnumerable<TPrevEnumerable, TPrevEnumerator, TSource, TPredicate>, Enumerator, RangeRepeatEnumerable<TSource, TAction>, RangeRepeatEnumerable<TSource, TAction>.Enumerator, TSource>(this, second);

        public ConcatEnumerable<
                WhereEnumerable<TPrevEnumerable, TPrevEnumerator, TSource, TPredicate>,
                Enumerator,
                SelectEnumerable<TEnumerable1, TEnumerator1, TPrevSource, TSource, TAction>,
                SelectEnumerable<TEnumerable1, TEnumerator1, TPrevSource, TSource, TAction>.Enumerator,
                TSource
            >
            Concat<TEnumerable1, TEnumerator1, TPrevSource, TAction>
            (in SelectEnumerable<TEnumerable1, TEnumerator1, TPrevSource, TSource, TAction> second)
            where TEnumerable1 : struct, IRefEnumerable<TEnumerator1, TPrevSource>
            where TEnumerator1 : struct, IRefEnumerator<TPrevSource>
            where TPrevSource : unmanaged
            where TAction : unmanaged, IRefAction<TPrevSource, TSource>
            => new ConcatEnumerable<
                    WhereEnumerable<TPrevEnumerable, TPrevEnumerator, TSource, TPredicate>,
                    Enumerator,
                    SelectEnumerable<TEnumerable1, TEnumerator1, TPrevSource, TSource, TAction>,
                    SelectEnumerable<TEnumerable1, TEnumerator1, TPrevSource, TSource, TAction>.Enumerator,
                    TSource>
                (this, second);

        public ConcatEnumerable<
                WhereEnumerable<TPrevEnumerable, TPrevEnumerator, TSource, TPredicate>,
                Enumerator,
                SelectIndexEnumerable<TEnumerable1, TEnumerator1, TPrevSource, TSource, TAction>,
                SelectIndexEnumerable<TEnumerable1, TEnumerator1, TPrevSource, TSource, TAction>.Enumerator,
                TSource
            >
            Concat<TEnumerable1, TEnumerator1, TPrevSource, TAction>
            (in SelectIndexEnumerable<TEnumerable1, TEnumerator1, TPrevSource, TSource, TAction> second)
            where TEnumerable1 : struct, IRefEnumerable<TEnumerator1, TPrevSource>
            where TEnumerator1 : struct, IRefEnumerator<TPrevSource>
            where TPrevSource : unmanaged
            where TAction : unmanaged, ISelectIndex<TPrevSource, TSource>
            => new ConcatEnumerable<
                    WhereEnumerable<TPrevEnumerable, TPrevEnumerator, TSource, TPredicate>,
                    Enumerator,
                    SelectIndexEnumerable<TEnumerable1, TEnumerator1, TPrevSource, TSource, TAction>,
                    SelectIndexEnumerable<TEnumerable1, TEnumerator1, TPrevSource, TSource, TAction>.Enumerator,
                    TSource>
                (this, second);

        public ConcatEnumerable<
                WhereEnumerable<TPrevEnumerable, TPrevEnumerator, TSource, TPredicate>,
                Enumerator,
                SelectManyEnumerable<TEnumerable0, TEnumerator0, TPrevSource0, TSource, TResultEnumerable0, TResultEnumerator0, TAction0>,
                SelectManyEnumerable<TEnumerable0, TEnumerator0, TPrevSource0, TSource, TResultEnumerable0, TResultEnumerator0, TAction0>.Enumerator,
                TSource
            >
            Concat<TEnumerable0, TEnumerator0, TPrevSource0, TResultEnumerable0, TResultEnumerator0, TAction0>
            (in SelectManyEnumerable<TEnumerable0, TEnumerator0, TPrevSource0, TSource, TResultEnumerable0, TResultEnumerator0, TAction0> second)
            where TEnumerable0 : struct, IRefEnumerable<TEnumerator0, TPrevSource0>
            where TEnumerator0 : struct, IRefEnumerator<TPrevSource0>
            where TPrevSource0 : unmanaged
            where TResultEnumerator0 : struct, IRefEnumerator<TSource>
            where TResultEnumerable0 : struct, IRefEnumerable<TResultEnumerator0, TSource>
            where TAction0 : struct, IRefAction<TPrevSource0, TResultEnumerable0>
            => new ConcatEnumerable<WhereEnumerable<TPrevEnumerable, TPrevEnumerator, TSource, TPredicate>, Enumerator, SelectManyEnumerable<TEnumerable0, TEnumerator0, TPrevSource0, TSource, TResultEnumerable0, TResultEnumerator0, TAction0>, SelectManyEnumerable<TEnumerable0, TEnumerator0, TPrevSource0, TSource, TResultEnumerable0, TResultEnumerator0, TAction0>.Enumerator, TSource>(this, second);

        public ConcatEnumerable<
                WhereEnumerable<TPrevEnumerable, TPrevEnumerator, TSource, TPredicate>,
                Enumerator,
                WhereEnumerable<TEnumerable1, TEnumerator1, TSource, TPredicate1>,
                WhereEnumerable<TEnumerable1, TEnumerator1, TSource, TPredicate1>.Enumerator,
                TSource
            >
            Concat<TEnumerable1, TEnumerator1, TPredicate1>
            (in WhereEnumerable<TEnumerable1, TEnumerator1, TSource, TPredicate1> second)
            where TEnumerable1 : struct, IRefEnumerable<TEnumerator1, TSource>
            where TEnumerator1 : struct, IRefEnumerator<TSource>
            where TPredicate1 : unmanaged, IRefFunc<TSource, bool>
            => new ConcatEnumerable<WhereEnumerable<TPrevEnumerable, TPrevEnumerator, TSource, TPredicate>, Enumerator, WhereEnumerable<TEnumerable1, TEnumerator1, TSource, TPredicate1>, WhereEnumerable<TEnumerable1, TEnumerator1, TSource, TPredicate1>.Enumerator, TSource>(this, second);

        public ConcatEnumerable<
                WhereEnumerable<TPrevEnumerable, TPrevEnumerator, TSource, TPredicate>,
                Enumerator,
                WhereIndexEnumerable<TEnumerable1, TEnumerator1, TSource, TPredicate1>,
                WhereIndexEnumerable<TEnumerable1, TEnumerator1, TSource, TPredicate1>.Enumerator,
                TSource
            >
            Concat<TEnumerable1, TEnumerator1, TPredicate1>
            (in WhereIndexEnumerable<TEnumerable1, TEnumerator1, TSource, TPredicate1> second)
            where TEnumerable1 : struct, IRefEnumerable<TEnumerator1, TSource>
            where TEnumerator1 : struct, IRefEnumerator<TSource>
            where TPredicate1 : unmanaged, IWhereIndex<TSource>
            => new ConcatEnumerable<WhereEnumerable<TPrevEnumerable, TPrevEnumerator, TSource, TPredicate>, Enumerator, WhereIndexEnumerable<TEnumerable1, TEnumerator1, TSource, TPredicate1>, WhereIndexEnumerable<TEnumerable1, TEnumerator1, TSource, TPredicate1>.Enumerator, TSource>(this, second);
        
        public ConcatEnumerable<
                WhereEnumerable<TPrevEnumerable, TPrevEnumerator, TSource, TPredicate>,
                Enumerator,
                ZipEnumerable<TEnumerable0, TEnumerator0, TSource0, TEnumerable1, TEnumerator1, TSource1, TSource, TAction0>,
                ZipEnumerable<TEnumerable0, TEnumerator0, TSource0, TEnumerable1, TEnumerator1, TSource1, TSource, TAction0>.Enumerator,
                TSource
            >
            Concat<TEnumerable0, TEnumerator0, TSource0, TEnumerable1, TEnumerator1, TSource1, TAction0>
            (in ZipEnumerable<TEnumerable0, TEnumerator0, TSource0, TEnumerable1, TEnumerator1, TSource1, TSource, TAction0> second)
            where TSource0 : unmanaged
            where TEnumerator0 : struct, IRefEnumerator<TSource0>
            where TEnumerable0 : struct, IRefEnumerable<TEnumerator0, TSource0>
            where TSource1 : unmanaged
            where TEnumerator1 : struct, IRefEnumerator<TSource1>
            where TEnumerable1 : struct, IRefEnumerable<TEnumerator1, TSource1>
            where TAction0 : struct, IRefAction<TSource0, TSource1, TSource>
            => new ConcatEnumerable<WhereEnumerable<TPrevEnumerable, TPrevEnumerator, TSource, TPredicate>, Enumerator, ZipEnumerable<TEnumerable0, TEnumerator0, TSource0, TEnumerable1, TEnumerator1, TSource1, TSource, TAction0>, ZipEnumerable<TEnumerable0, TEnumerator0, TSource0, TEnumerable1, TEnumerator1, TSource1, TSource, TAction0>.Enumerator, TSource>(this, second);
        #endregion

        #region Function
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool CanFastCount() => false;

        public bool Any()
            => this.Any<WhereEnumerable<TPrevEnumerable, TPrevEnumerator, TSource, TPredicate>, Enumerator, TSource>();

        public bool Any<TAnotherPredicate>(TAnotherPredicate predicate)
            where TAnotherPredicate : unmanaged, IRefFunc<TSource, bool>
            => this.Any<WhereEnumerable<TPrevEnumerable, TPrevEnumerator, TSource, TPredicate>, Enumerator, TSource, TAnotherPredicate>(predicate);

        public bool Any(Func<TSource, bool> predicate)
            => this.Any<WhereEnumerable<TPrevEnumerable, TPrevEnumerator, TSource, TPredicate>, Enumerator, TSource>(predicate);

        public bool All<TAnotherPredicate>(TAnotherPredicate predicate)
            where TAnotherPredicate : unmanaged, IRefFunc<TSource, bool>
            => this.All<WhereEnumerable<TPrevEnumerable, TPrevEnumerator, TSource, TPredicate>, Enumerator, TSource, TAnotherPredicate>(predicate);

        public bool All(Func<TSource, bool> predicate)
            => this.All<WhereEnumerable<TPrevEnumerable, TPrevEnumerator, TSource, TPredicate>, Enumerator, TSource>(predicate);

        public void Aggregate<TAccumulate, TFunc>(ref TAccumulate seed, TFunc func)
            where TFunc : unmanaged, IRefAction<TAccumulate, TSource>
            => this.Aggregate<WhereEnumerable<TPrevEnumerable, TPrevEnumerator, TSource, TPredicate>, Enumerator, TSource, TAccumulate, TFunc>(ref seed, func);

        public TResult Aggregate<TAccumulate, TResult, TFunc, TResultFunc>(ref TAccumulate seed, TFunc func, TResultFunc resultFunc)
            where TFunc : unmanaged, IRefAction<TAccumulate, TSource>
            where TResultFunc : unmanaged, IRefFunc<TAccumulate, TResult>
            => this.Aggregate<WhereEnumerable<TPrevEnumerable, TPrevEnumerator, TSource, TPredicate>, Enumerator, TSource, TAccumulate, TResult, TFunc, TResultFunc>(ref seed, func, resultFunc);

        public TSource Aggregate(Func<TSource, TSource, TSource> func)
            => this.Aggregate<WhereEnumerable<TPrevEnumerable, TPrevEnumerator, TSource, TPredicate>, Enumerator, TSource>(func);

        public TAccumulate Aggregate<TAccumulate>(TAccumulate seed, Func<TAccumulate, TSource, TAccumulate> func)
            => this.Aggregate<WhereEnumerable<TPrevEnumerable, TPrevEnumerator, TSource, TPredicate>, Enumerator, TSource, TAccumulate>(seed, func);

        public TResult Aggregate<TAccumulate, TResult>(TAccumulate seed, Func<TAccumulate, TSource, TAccumulate> func, Func<TAccumulate, TResult> resultFunc)
            => this.Aggregate<WhereEnumerable<TPrevEnumerable, TPrevEnumerator, TSource, TPredicate>, Enumerator, TSource, TAccumulate, TResult>(seed, func, resultFunc);

        public bool Contains(TSource value)
            => this.Contains<WhereEnumerable<TPrevEnumerable, TPrevEnumerator, TSource, TPredicate>, Enumerator, TSource>(value);

        public bool Contains(TSource value, IEqualityComparer<TSource> comparer)
            => this.Contains<WhereEnumerable<TPrevEnumerable, TPrevEnumerator, TSource, TPredicate>, Enumerator, TSource>(value, comparer);

        public bool Contains<TComparer>(TSource value, TComparer comparer)
            where TComparer : unmanaged, IRefFunc<TSource, TSource, bool>
            => this.Contains<WhereEnumerable<TPrevEnumerable, TPrevEnumerator, TSource, TPredicate>, Enumerator, TSource, TComparer>(value, comparer);

        public int Count()
            => this.Count<WhereEnumerable<TPrevEnumerable, TPrevEnumerator, TSource, TPredicate>, Enumerator, TSource>();

        public int Count(Func<TSource, bool> predicate)
            => this.Count<WhereEnumerable<TPrevEnumerable, TPrevEnumerator, TSource, TPredicate>, Enumerator, TSource>(predicate);

        public int Count<TAnotherPredicate>(TAnotherPredicate predicate)
            where TAnotherPredicate : unmanaged, IRefFunc<TSource, bool>
            => this.Count<WhereEnumerable<TPrevEnumerable, TPrevEnumerator, TSource, TPredicate>, Enumerator, TSource, TAnotherPredicate>(predicate);

        public long LongCount()
            => this.LongCount<WhereEnumerable<TPrevEnumerable, TPrevEnumerator, TSource, TPredicate>, Enumerator, TSource>();

        public long LongCount(Func<TSource, bool> predicate)
            => this.LongCount<WhereEnumerable<TPrevEnumerable, TPrevEnumerator, TSource, TPredicate>, Enumerator, TSource>(predicate);

        public long LongCount<TAnotherPredicate>(TAnotherPredicate predicate)
            where TAnotherPredicate : unmanaged, IRefFunc<TSource, bool>
            => this.LongCount<WhereEnumerable<TPrevEnumerable, TPrevEnumerator, TSource, TPredicate>, Enumerator, TSource, TAnotherPredicate>(predicate);

        public bool TryGetElementAt(long index, out TSource element)
            => this.TryGetElementAt<WhereEnumerable<TPrevEnumerable, TPrevEnumerator, TSource, TPredicate>, Enumerator, TSource>(index, out element);

        public NativeEnumerable<TSource> ToNativeEnumerable(Allocator allocator)
            => this.ToNativeEnumerable<
                WhereEnumerable<TPrevEnumerable, TPrevEnumerator, TSource, TPredicate>
                ,Enumerator, TSource>(allocator);

        public bool TryGetFirst(out TSource first)
            => this.TryGetFirst<WhereEnumerable<TPrevEnumerable, TPrevEnumerator, TSource, TPredicate>, Enumerator, TSource>(out first);

        public bool TryGetLast(out TSource last)
            => this.TryGetLast<WhereEnumerable<TPrevEnumerable, TPrevEnumerator, TSource, TPredicate>, Enumerator, TSource>(out last);

        public bool TryGetSingle(out TSource value)
            => this.TryGetSingle<WhereEnumerable<TPrevEnumerable, TPrevEnumerator, TSource, TPredicate>, Enumerator, TSource>(out value);

        public bool TryGetSingle<TAnotherPredicate>(out TSource value, TAnotherPredicate predicate)
            where TAnotherPredicate : unmanaged, IRefFunc<TSource, bool>
            => this.TryGetSingle<WhereEnumerable<TPrevEnumerable, TPrevEnumerator, TSource, TPredicate>, Enumerator, TSource, TAnotherPredicate>(out value, predicate);

        public bool TryGetSingle(out TSource value, Func<TSource, bool> predicate)
            => this.TryGetSingle<WhereEnumerable<TPrevEnumerable, TPrevEnumerator, TSource, TPredicate>, Enumerator, TSource>(out value, predicate);

        public TSource[] ToArray()
            => this.ToArray<WhereEnumerable<TPrevEnumerable, TPrevEnumerator, TSource, TPredicate>, Enumerator, TSource>();

        public NativeArray<TSource> ToNativeArray(Allocator alloc)
            => this.ToNativeArray<WhereEnumerable<TPrevEnumerable, TPrevEnumerator, TSource, TPredicate>, Enumerator, TSource>(alloc);

        public Dictionary<TKey, TElement> ToDictionary<TKey, TElement>(Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector)
            => this.ToDictionary<WhereEnumerable<TPrevEnumerable, TPrevEnumerator, TSource, TPredicate>, Enumerator, TSource, TKey, TElement>(keySelector, elementSelector);

        public Dictionary<TKey, TElement> ToDictionary<TKey, TElement, TKeyFunc, TElementFunc>(TKeyFunc keySelector, TElementFunc elementSelector)
            where TKeyFunc : unmanaged, IRefFunc<TSource, TKey>
            where TElementFunc : unmanaged, IRefFunc<TSource, TElement>
            => this.ToDictionary<WhereEnumerable<TPrevEnumerable, TPrevEnumerator, TSource, TPredicate>, Enumerator, TSource, TKey, TElement, TKeyFunc, TElementFunc>(keySelector, elementSelector);

        public HashSet<TSource> ToHashSet()
            => this.ToHashSet<WhereEnumerable<TPrevEnumerable, TPrevEnumerator, TSource, TPredicate>, Enumerator, TSource>();

        public HashSet<TSource> ToHashSet(IEqualityComparer<TSource> comparer)
            => this.ToHashSet<WhereEnumerable<TPrevEnumerable, TPrevEnumerator, TSource, TPredicate>, Enumerator, TSource>(comparer);

        public List<TSource> ToList()
            => this.ToList<WhereEnumerable<TPrevEnumerable, TPrevEnumerator, TSource, TPredicate>, Enumerator, TSource>();
        #endregion

        public struct Enumerator : IRefEnumerator<TSource>
        {
            private TPrevEnumerator enumerator;
            private TPredicate predicts;

            internal Enumerator(in TPrevEnumerator enumerator, TPredicate predicts)
            {
                this.enumerator = enumerator;
                this.predicts = predicts;
            }

            public bool MoveNext()
            {
                while (enumerator.MoveNext())
                    if (predicts.Calc(ref enumerator.Current))
                        return true;
                return false;
            }

            public void Reset() => throw new InvalidOperationException();

            public ref TSource Current => ref enumerator.Current;

            TSource IEnumerator<TSource>.Current => Current;

            object IEnumerator.Current => Current;

            public void Dispose() => enumerator.Dispose();
        }
    }
}